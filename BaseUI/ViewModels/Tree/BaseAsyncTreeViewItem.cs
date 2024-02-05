namespace BaseUI.ViewModels.Tree;

/// <summary>
///     Base tree view item that loads its children asynchronously.
/// </summary>
public abstract class BaseAsyncTreeViewItem(bool isExpandable = false) : BaseTreeViewItem(isExpandable)
{
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
        Application.Current.Dispatcher.Invoke(() => { base.SetChildren(childs); });
    }
}