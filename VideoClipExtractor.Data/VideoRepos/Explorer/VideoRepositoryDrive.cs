namespace VideoClipExtractor.Data.VideoRepos.Explorer;

public abstract class VideoRepositoryDrive : VideoRepositoryItem
{
    protected VideoRepositoryDrive(string name)
    {
        Name = name;
        Path = name;
    }
}