using BaseUI.Services.DependencyInjection;
using Moq;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class DependencyMock : Mock<IDependencyProvider>
{
    public void AddMockDependency<TDependency>(Mock<TDependency> mock) where TDependency : class =>
        Setup(d => d.GetDependency<TDependency>()).Returns(mock.Object);
}