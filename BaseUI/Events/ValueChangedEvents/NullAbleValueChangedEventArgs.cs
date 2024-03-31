namespace BaseUI.Events.ValueChangedEvents;

public class NullAbleValueChangedEventArgs<T>(T? changedProperty) : EventArgs
{
    public T? ChangedProperty { get; set; } = changedProperty;
}