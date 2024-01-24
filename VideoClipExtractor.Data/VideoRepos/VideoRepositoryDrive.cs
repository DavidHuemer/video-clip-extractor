using System.Collections.ObjectModel;

namespace VideoClipExtractor.Data.VideoRepos;

public abstract class VideoRepositoryDrive : VideoRepositoryItem
{
    protected VideoRepositoryDrive(string name)
    {
        Name = name;
        Path = name;
    }
}