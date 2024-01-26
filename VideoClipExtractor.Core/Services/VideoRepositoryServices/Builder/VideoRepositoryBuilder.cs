using System.Runtime.Versioning;
using JetBrains.Annotations;
using MediaDevices;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.Data.VideoRepos.Implementations;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.Builder;

[UsedImplicitly]
public class VideoRepositoryBuilder : IVideoRepositoryBuilder
{
    public IVideoRepository Build(VideoRepositoryBlueprint blueprint)
    {
        return blueprint.Type switch
        {
            VideoRepositoryType.Pc => BuildPcConnection(blueprint),
            VideoRepositoryType.Phone when OperatingSystem.IsWindows() => BuildPhoneConnection(blueprint),
            _ => throw new ArgumentOutOfRangeException(nameof(blueprint), "Invalid connection type"),
        };
    }

    private static PcVideoRepository BuildPcConnection(VideoRepositoryBlueprint connectionBlueprint) =>
        new(connectionBlueprint.Path);

    [SupportedOSPlatform("windows")]
    private static PhoneVideoRepository BuildPhoneConnection(VideoRepositoryBlueprint connectionBlueprint)
    {
        var connectedDevices = MediaDevice.GetDevices();
        var deviceName = connectionBlueprint.Path.Split('\\')[0];

        var device = connectedDevices.First(d => d.FriendlyName == deviceName);
        return new PhoneVideoRepository(device, connectionBlueprint.Path);
    }
}