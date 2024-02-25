using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.Provider.DependencyFinderService;
using BaseUI.Services.Provider.ViewModelProvider;
using Moq;
using VideoClipExtractor.Tests.Basics.BaseTests;

namespace VideoClipExtractor.Tests.BaseUI.Services.Provider.ViewModelProviderTests;

public class ViewModelProviderTests : BaseDependencyTest
{
    private ViewModelProvider _viewModelProvider = null!;
    private Mock<IDependencyFinder> _dependencyFinder = null!;
    
    
    public override void Setup()
    {
        base.Setup();
        _dependencyFinder = DependencyMock.CreateMockDependency<IDependencyFinder>();
        _viewModelProvider = new ViewModelProvider(DependencyMock.Object);
    }

    [Test]
    public void NotRegisteredThrowsException()
    {
        Assert.Throws<DependencyNotRegisteredException>(() => _viewModelProvider.Get<INotExistingViewModel>());
    }

    [Test]
    public void RegisteredViewModelIsReturned()
    {
        _viewModelProvider.AddSingleton<IExistingViewModel, ExistingViewModel>();
        var viewModel = _viewModelProvider.Get<IExistingViewModel>();
        Assert.NotNull(viewModel);
        Assert.IsInstanceOf<ExistingViewModel>(viewModel);
    }

    [Test]
    public void AutoResolveViewModel()
    {
        _dependencyFinder.Setup(x => x.FindDependency<IExistingViewModel>()).Returns(typeof(ExistingViewModel));
        var viewModel = _viewModelProvider.Get<IExistingViewModel>();
        Assert.NotNull(viewModel);
        Assert.IsInstanceOf<ExistingViewModel>(viewModel);
    }
}