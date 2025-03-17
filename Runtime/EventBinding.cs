using System;

internal readonly struct EventBinding<T> : IEquatable<EventBinding<T>> where T : IEvent {
    
    public Action<T> OnEvent { get; }
    public Action OnEventNoArgs { get; }
    
    private EventBinding(Action<T> onEvent)
    {
        OnEvent = onEvent;
        OnEventNoArgs = () => {};
    }

    private EventBinding(Action onEventNoArgs)
    {
        OnEvent = _ => {};
        OnEventNoArgs = onEventNoArgs;
    }

    public bool Equals(EventBinding<T> other) =>
        Equals(OnEvent, other.OnEvent) && Equals(OnEventNoArgs, other.OnEventNoArgs);

    public override bool Equals(object obj) =>
        obj is EventBinding<T> other && Equals(other);
    
    public override int GetHashCode() =>
        HashCode.Combine(OnEvent, OnEventNoArgs);
    
    public static explicit operator EventBinding<T>(Action<T> action) => new (action);
    public static explicit operator EventBinding<T>(Action actionNoArgs) => new (actionNoArgs);
}
