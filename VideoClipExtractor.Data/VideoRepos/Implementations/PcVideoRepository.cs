using System.IO;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Data.VideoRepos.Implementations;

public class PcVideoRepository(string path) : IVideoRepository
{
    public void Connect()
    {
    }

    public void CopyFileByPath(string sourceVideoPath, string cacheVideoPath)
        => File.Copy(sourceVideoPath, cacheVideoPath);

    public IEnumerable<SourceVideo> GetFiles()
    {
        return Directory.GetFiles(path)
            .Select(file => new FileInfo(file))
            .Where(file => VideoFileTypes.IsSupported(file.Extension.TrimStart('.')))
            .Select(file => new SourceVideo(file));
    }
}