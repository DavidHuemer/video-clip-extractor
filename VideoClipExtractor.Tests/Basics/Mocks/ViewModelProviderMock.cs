using BaseUI.Services.ViewModelProvider;
using Moq;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class ViewModelProviderMock : Mock<IViewModelProvider>
{
    public void AddViewModel<TDependency>(Mock<TDependency> mock) where TDependency : class
    {
        Setup(m => m.GetViewModel<TDependency>()).Returns(mock.Object);
    }
}