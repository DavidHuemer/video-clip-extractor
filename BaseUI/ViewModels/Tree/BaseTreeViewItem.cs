using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace BaseUI.ViewModels.Tree;

/// <summary>
///     Base class for all tree view items.
/// </summary>
public abstract class BaseTreeViewItem(bool isExpandable = false) : INotifyPropertyChanged
{
    protected virtual void ExpansionChanged()
    {
        var children = LoadChildren();
        SetChildren(children);
    }

    /// <summary>
    ///     Sets the children to the given children.
    /// </summary>
    /// <param name="children">The children that should be set as the tree view items</param>
    protected virtual void SetChildren(IEnumerable<BaseTreeViewItem> children)
    {
        Children.Clear();
        foreach (var item in children) Children.Add(item);
    }

    #region Abstract Methods

    /// <summary>
    ///     Loads the children of the item.
    /// </summary>
    /// <returns>The children of the item</returns>
    protected abstract IEnumerable<BaseTreeViewItem> LoadChildren();

    #endregion

    /// <summary>
    ///     Returns the initial children of the item, depending on whether it is expandable or not.
    /// </summary>
    /// <param name="isExpandable">Whether the item is expandable or not</param>
    /// <returns>The initial children</returns>
    private static IEnumerable<BaseTreeViewItem> GetInitialChildren(bool isExpandable)
    {
        return isExpandable ? [new DummyTreeViewItem()] : Array.Empty<BaseTreeViewItem>();
    }

    #region Notify

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region Properties

    /// <summary>
    ///     Whether the item can be expanded or not. (e.g. a folder can be expanded, but a file cannot)
    /// </summary>
    [DoNotNotify]
    protected bool IsExpandable { get; private set; } = isExpandable;

    private bool _isExpanded;

    /// <summary>
    ///     Whether the item is expanded or not.
    /// </summary>
    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            _isExpanded = value;
            OnPropertyChanged();
            ExpansionChanged();
        }
    }

    /// <summary>
    ///     The children of the item.
    /// </summary>
    public ObservableCollection<BaseTreeViewItem> Children { get; set; } = new(GetInitialChildren(isExpandable));

    #endregion
}