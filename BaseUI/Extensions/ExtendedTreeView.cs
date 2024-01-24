namespace BaseUI.Extensions;

/// <summary>
/// Extended <see cref="TreeView"/>
/// </summary>
public class ExtendedTreeView : TreeView
{
    public new static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(ExtendedTreeView)
        );

    public ExtendedTreeView()
    {
        SelectedItemChanged += OnSelectedItemChanged;
    }

    public new object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        SelectedItem = e.NewValue;
    }
}