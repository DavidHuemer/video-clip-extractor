using System.Collections.ObjectModel;
using BaseUI.ViewModels.Tree;
using MediaDevices;

namespace VideoClipExtractor.Data.VideoRepos.Phone;

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

    public IEnumerable<string> GetFiles(string path)
    {
        return _device.GetFiles(path);
    }

    protected override IEnumerable<BaseTreeViewItem> LoadChildren() =>
        GetDirectories(Path).Select(dir => new PhoneDirectory(this, dir));
}