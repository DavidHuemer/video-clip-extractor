using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Managers.WorkspaceManager;

/// <summary>
/// Responsible for managing the current workspace.
/// <para>The workspace consists of thw current videos</para>
/// </summary>
public interface IWorkspaceManager
{
    /**
     * Signals that the workspace has been cleared.
     */
    event EventHandler Clear;

    event Action<VideoViewModel> VideoAdded;

    void SourceVideosChanged();
}