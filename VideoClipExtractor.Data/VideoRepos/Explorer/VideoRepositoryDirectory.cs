namespace VideoClipExtractor.Data.VideoRepos.Explorer;

public abstract class VideoRepositoryDirectory : VideoRepositoryItem
{
    protected VideoRepositoryDirectory(string name)
    {
        Path = name;
        Name = name;
    }
}