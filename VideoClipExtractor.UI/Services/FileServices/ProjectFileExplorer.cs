using BaseUI.Data;
using BaseUI.Services.FileServices.Implementations;
using BaseUI.Services.Provider.DependencyInjection;

namespace VideoClipExtractor.UI.Services.FileServices;

// ReSharper disable once ClassNeverInstantiated.Global
public class ProjectFileExplorer(IDependencyProvider provider) : BaseProjectFileExplorer(provider,
    new FileTypeInfo("VideoClipExtractor", "vce"));