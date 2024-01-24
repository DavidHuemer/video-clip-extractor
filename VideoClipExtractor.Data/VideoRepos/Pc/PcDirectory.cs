using System.IO;
using BaseUI.ViewModels.Tree;

namespace VideoClipExtractor.Data.VideoRepos.Pc;

public class PcDirectory : VideoRepositoryDirectory
{
    public PcDirectory(string name) : base(name)
    {
        Path = name;
        Name = new DirectoryInfo(Path).Name;
    }

    protected override IEnumerable<BaseTreeViewItem> LoadChildren() => Directory.GetDirectories(Path)
        .Select(dir => new PcDirectory(dir));
}