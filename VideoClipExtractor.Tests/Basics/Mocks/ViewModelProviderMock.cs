using BaseUI.Services.Provider.ViewModelProvider;
using Moq;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class ViewModelProviderMock : Mock<IViewModelProvider>
{
    public void AddViewModel<TDependency>(Mock<TDependency> mock) where TDependency : class
    {
        Setup(m => m.Get<TDependency>()).Returns(mock.Object);
    }

    public Mock<TViewModel> CreateViewModelMock<TViewModel>() where TViewModel : class
    {
        var mock = new Mock<TViewModel>();
        AddViewModel(mock);
        return mock;
    }
}