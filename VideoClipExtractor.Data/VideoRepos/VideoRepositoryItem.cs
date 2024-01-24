using System.Collections.ObjectModel;
using BaseUI.ViewModels.Tree;

namespace VideoClipExtractor.Data.VideoRepos;

public abstract class VideoRepositoryItem : BaseAsyncTreeViewItem
{
    #region Properties

    public string Name { get; protected init; } = "";

    protected string Path { get; init; } = "";

    #endregion

    public VideoRepositoryItem() : base(true)
    {
        
    }
}