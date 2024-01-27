namespace VideoClipExtractor.Data.Project;

public class OpenRecentlyOpenedEventArgs(string recentlyOpenedPath) : EventArgs
{
    public string RecentlyOpenedPath { get; set; } = recentlyOpenedPath;
}