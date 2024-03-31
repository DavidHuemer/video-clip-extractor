namespace BaseUI.Events.ValueChangedEvents;

public abstract class ValueChangedEventArgs<T>(T changedProperty) : EventArgs
{
    public T ChangedProperty { get; set; } = changedProperty;
}