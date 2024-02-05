using System.IO;
using BaseUI.ViewModels.Tree;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Data.VideoRepos.Explorer.Pc;

public class PcDirectory : VideoRepositoryDirectory
{
    public PcDirectory(string name) : base(name)
    {
        Path = name;
        Name = new DirectoryInfo(Path).Name;
    }

    protected override IEnumerable<BaseTreeViewItem> LoadChildren()
    {
        return Directory.GetDirectories(Path)
            .Select(dir => new PcDirectory(dir));
    }

    public override VideoRepositoryBlueprint GetBlueprint()
    {
        return new VideoRepositoryBlueprint(VideoRepositoryType.Pc, Path);
    }
}