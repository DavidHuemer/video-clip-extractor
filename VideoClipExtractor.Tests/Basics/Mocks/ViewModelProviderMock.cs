using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using Moq;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class ViewModelProviderMock : Mock<IViewModelProvider>
{
    public void AddViewModel<TDependency>(Mock<TDependency> mock) where TDependency : class
    {
        Setup(m => m.Get<TDependency>()).Returns(mock.Object);
    }

    public ViewModelMock<TViewModel> CreateViewModelMock<TViewModel>() where TViewModel : class, IBaseViewModel
    {
        var mock = new ViewModelMock<TViewModel>();
        Setup(m => m.Get<TViewModel>()).Returns(mock.Object);
        return mock;
    }
}