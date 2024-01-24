using System.Collections.ObjectModel;

namespace VideoClipExtractor.Data.VideoRepos;

public abstract class VideoRepositoryDirectory : VideoRepositoryItem
{
    protected VideoRepositoryDirectory(string name)
    {
        Path = name;
        Name = name;
    }
}