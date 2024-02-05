using System.IO;
using System.Runtime.Versioning;
using JetBrains.Annotations;
using MediaDevices;
using VideoClipExtractor.Data.VideoRepos.Explorer;
using VideoClipExtractor.Data.VideoRepos.Explorer.Pc;
using VideoClipExtractor.Data.VideoRepos.Explorer.Phone;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.Provider;

[UsedImplicitly]
public class VideoRepositoryProvider : IVideoRepositoryProvider
{
    public IEnumerable<VideoRepositoryDrive> GetDrives()
    {
        return GetPcDrives()
            .Concat(OperatingSystem.IsWindows()
                ? GetPhoneDrives()
                : Enumerable.Empty<VideoRepositoryDrive>());
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