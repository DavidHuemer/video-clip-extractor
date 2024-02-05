namespace BaseUI.ViewModels.Tree;

public class DummyTreeViewItem : BaseTreeViewItem
{
    public DummyTreeViewItem()
    {
        IsExpanded = false;
    }

    protected override IEnumerable<BaseTreeViewItem> LoadChildren()
    {
        return Array.Empty<BaseTreeViewItem>();
    }
}