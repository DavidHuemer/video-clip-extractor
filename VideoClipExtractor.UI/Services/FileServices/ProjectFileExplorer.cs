using BaseUI.Data;
using BaseUI.Services.DependencyInjection;
using BaseUI.Services.FileServices.Implementations;

namespace VideoClipExtractor.UI.Services.FileServices;

// ReSharper disable once ClassNeverInstantiated.Global
public class ProjectFileExplorer(IDependencyProvider provider) : BaseProjectFileExplorer(provider,
    new FileTypeInfo("VideoClipExtractor", "vce"));