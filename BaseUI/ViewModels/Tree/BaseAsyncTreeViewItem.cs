namespace BaseUI.ViewModels.Tree;

/// <summary>
/// Base tree view item that loads its children asynchronously.
/// </summary>
public abstract class BaseAsyncTreeViewItem : BaseTreeViewItem
{
    protected BaseAsyncTreeViewItem(bool isExpandable = false) : base(isExpandable)
    {
    }

    protected override void ExpansionChanged()
    {
        Task.Run(() =>
        {
            var children = LoadChildren();
            SetChildren(children);
        });
    }

    protected override void SetChildren(IEnumerable<BaseTreeViewItem> childs)
    {
        // Run on UI thread
        System.Windows.Application.Current.Dispatcher.Invoke(() => { base.SetChildren(childs); });
    }
}