using BaseUI.ViewModels.Tree;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Data.VideoRepos.Explorer.Phone;

public class PhoneDirectory : VideoRepositoryDirectory
{
    #region Private Fields

    private readonly PhoneDrive _drive;

    #endregion

    public PhoneDirectory(PhoneDrive drive, string path) : base(path)
    {
        _drive = drive;
        Path = path;

        // The name should be the last part of the path
        Name = Path.Split('\\').Last();
    }

    protected override IEnumerable<BaseTreeViewItem> LoadChildren() =>
        _drive.GetDirectories(Path).Select(dir => new PhoneDirectory(_drive, dir));

    public override VideoRepositoryBlueprint GetBlueprint() => new(VideoRepositoryType.Phone, _drive.ConcatPath(Path));
}