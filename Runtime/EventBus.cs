using System;
using System.Collections.Generic;
using System.Linq;

public static class EventBus<T> where T : IEvent
{
    private static readonly HashSet<EventBinding<T>> Bindings = new ();

    public static void Register(Action<T> callback) => Bindings.Add((EventBinding<T>)callback);
    public static void Register(Action callbackNoArgs) => Bindings.Add((EventBinding<T>)callbackNoArgs);
    public static void Deregister(Action<T> callback) => Bindings.Remove((EventBinding<T>)callback);
    public static void Deregister(Action callbackNoArgs) => Bindings.Remove((EventBinding<T>)callbackNoArgs);

    public static void Raise(T @event)
    {
        HashSet<EventBinding<T>> snapshot = new (Bindings);

        foreach (EventBinding<T> binding in snapshot.Where(binding => Bindings.Contains(binding)))
        {
            binding.OnEvent.Invoke(@event);
            binding.OnEventNoArgs.Invoke();
        }
    }
}
