using System.IO;
using System.Runtime.Versioning;
using MediaDevices;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.VideoRepos.Pc;
using VideoClipExtractor.Data.VideoRepos.Phone;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices;

public class VideoRepositoryProvider : IVideoRepositoryProvider
{
    public IEnumerable<VideoRepositoryDrive> GetDrives()
    {
        var drives = new List<VideoRepositoryDrive>();
        drives.AddRange(GetPcDrives());
        if (OperatingSystem.IsWindows()) drives.AddRange(GetPhoneDrives());
        return drives;
    }

    private static IEnumerable<VideoRepositoryDrive> GetPcDrives()
    {
        return DriveInfo.GetDrives().Select(driveInfo => new PcDrive(driveInfo.Name));
    }

    [SupportedOSPlatform("windows")]
    private static IEnumerable<VideoRepositoryDrive> GetPhoneDrives()
    {
        return MediaDevice.GetDevices().Select(device => new PhoneDrive(device));
    }
}