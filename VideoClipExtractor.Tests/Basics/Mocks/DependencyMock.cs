using BaseUI.Services.Provider.DependencyInjection;
using Moq;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class DependencyMock : Mock<IDependencyProvider>
{
    public void AddMockDependency<TDependency>(Mock<TDependency> mock) where TDependency : class
    {
        Setup(d => d.GetDependency<TDependency>()).Returns(mock.Object);
    }

    public ViewModelProviderMock AddViewModelProvider()
    {
        var mock = new ViewModelProviderMock();
        AddMockDependency(mock);
        return mock;
    }

    public Mock<TDependency> CreateMockDependency<TDependency>() where TDependency : class
    {
        var mock = new Mock<TDependency>();
        AddMockDependency(mock);
        return mock;
    }
}