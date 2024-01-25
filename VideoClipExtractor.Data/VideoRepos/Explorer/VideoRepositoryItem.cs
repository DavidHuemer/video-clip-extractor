using BaseUI.ViewModels.Tree;

namespace VideoClipExtractor.Data.VideoRepos.Explorer;

public abstract class VideoRepositoryItem() : BaseAsyncTreeViewItem(true)
{
    #region Properties

    public string Name { get; protected init; } = "";

    protected string Path { get; init; } = "";

    #endregion
}