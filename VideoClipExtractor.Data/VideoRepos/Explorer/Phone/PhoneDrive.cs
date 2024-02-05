using BaseUI.ViewModels.Tree;
using MediaDevices;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Data.VideoRepos.Explorer.Phone;

public class PhoneDrive : VideoRepositoryDrive
{
    #region Private Fields

    private readonly MediaDevice _device;

    #endregion

    public PhoneDrive(MediaDevice device) : base(device.FriendlyName)
    {
        _device = device;
        _device.Connect();
        Name = device.FriendlyName;
        Path = device.GetRootDirectory().Name;
    }

    public IEnumerable<string> GetDirectories(string path)
    {
        return _device.GetDirectories(path);
    }

    public string ConcatPath(string path)
    {
        return $@"{_device.FriendlyName}\{path}";
    }

    protected override IEnumerable<BaseTreeViewItem> LoadChildren()
    {
        return GetDirectories(Path).Select(dir => new PhoneDirectory(this, dir));
    }

    public override VideoRepositoryBlueprint GetBlueprint()
    {
        return new VideoRepositoryBlueprint(VideoRepositoryType.Phone, ConcatPath(Path));
    }
}