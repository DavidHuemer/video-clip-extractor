using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Tests.Basics.BaseTests;

namespace VideoClipExtractor.Tests.BaseUI.Services.Provider.ViewModelProviderTests;

public class ViewModelProviderTests : BaseDependencyTest
{
    private ViewModelProvider _viewModelProvider = null!;

    public override void Setup()
    {
        base.Setup();
        _viewModelProvider = new ViewModelProvider(DependencyMock.Object);
    }

    [Test]
    public void NotRegisteredThrowsException()
    {
        Assert.Throws<DependencyNotRegisteredException>(() => _viewModelProvider.GetViewModel<INotExistingViewModel>());
    }

    [Test]
    public void RegisteredViewModelIsReturned()
    {
        _viewModelProvider.AddSingletonViewModel<IExistingViewModel, ExistingViewModel>();
        var viewModel = _viewModelProvider.GetViewModel<IExistingViewModel>();
        Assert.NotNull(viewModel);
        Assert.IsInstanceOf<ExistingViewModel>(viewModel);
    }

    [Test]
    public void AutoResolveViewModel()
    {
        var viewModel = _viewModelProvider.GetViewModel<IExistingViewModel>();
        Assert.NotNull(viewModel);
        Assert.IsInstanceOf<ExistingViewModel>(viewModel);
    }
}