using Moq;
using VideoClipExtractor.Tests.Basics.Mocks;

namespace VideoClipExtractor.Tests.Basics.BaseTests;

/// <summary>
/// Base class for tests that have dependencies.
/// Provides a <see cref="DependencyMock"/>
/// </summary>
public abstract class BaseDependencyTest
{
    #region Protected Fields

    protected DependencyMock DependencyMock = null!;

    #endregion

    [SetUp]
    public virtual void Setup()
    {
        DependencyMock = new DependencyMock();
    }

    protected void AddMockDependency<T>(Mock<T> mock) where T : class => DependencyMock.AddMockDependency(mock);
}