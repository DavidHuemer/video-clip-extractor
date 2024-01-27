using BaseUI.Services.DependencyInjection;
using BaseUI.Services.RecentlyOpened;
using JetBrains.Annotations;

namespace VideoClipExtractor.Core.Services.RecentlyOpened;

[UsedImplicitly]
public class RecentlyOpenedFilesService(IDependencyProvider provider) : BaseRecentlyOpenedFilesService(provider)
{
    protected override string ProjectDirectory => "VideoClipExtractor";
}