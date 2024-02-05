using BaseUI.ViewModels.Tree;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Data.VideoRepos.Explorer;

public abstract class VideoRepositoryItem() : BaseAsyncTreeViewItem(true)
{
    /// <summary>
    ///     Returns a <see cref="VideoRepositoryBlueprint" /> out of this item.
    /// </summary>
    /// <returns>A <see cref="VideoRepositoryBlueprint" /> out the item.</returns>
    public abstract VideoRepositoryBlueprint GetBlueprint();

    #region Properties

    public string Name { get; protected init; } = "";

    protected string Path { get; init; } = "";

    #endregion
}