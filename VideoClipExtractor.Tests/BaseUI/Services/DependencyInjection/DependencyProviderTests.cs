using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.DependencyInjection;

namespace VideoClipExtractor.Tests.BaseUI.Services.DependencyInjection;

public class DependencyProviderTests
{
    private DependencyProvider _dependencyProvider = null!;

    // Before each test
    [SetUp]
    public void Setup()
    {
        Console.WriteLine("Setup");
        _dependencyProvider = new DependencyProvider();
    }

    [Test]
    public void ReturnNewInstanceOfTransientDependency()
    {
        // Arrange
        _dependencyProvider.AddTransientDependency<ITestInterface, TestImplementation>();

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.IsInstanceOf<TestImplementation>(testInterface);
    }

    [Test]
    public void TransientDependencyIsUpdated()
    {
        // Arrange
        _dependencyProvider.AddTransientDependency<ITestInterface, TestImplementation>();
        _dependencyProvider.AddTransientDependency<ITestInterface, SecondImplementation>();

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.IsInstanceOf<SecondImplementation>(testInterface);
    }

    [Test]
    public void ReturnSameInstanceOfSingletonDependency()
    {
        // Arrange
        _dependencyProvider.AddSingletonDependency<ITestInterface, TestImplementation>();

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();
        var testInterface2 = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.IsInstanceOf<TestImplementation>(testInterface);
        Assert.IsInstanceOf<TestImplementation>(testInterface2);
        Assert.That(testInterface2, Is.SameAs(testInterface));
    }

    [Test]
    public void SingletonDependencyIsUpdated()
    {
        // Arrange
        _dependencyProvider.AddSingletonDependency<ITestInterface, TestImplementation>();
        _dependencyProvider.AddSingletonDependency<ITestInterface, SecondImplementation>();

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.IsInstanceOf<SecondImplementation>(testInterface);
    }

    [Test]
    public void ThrowDependencyNotRegisteredException()
    {
        // Assert
        var exception =
            Assert.Throws<DependencyNotRegisteredException>(() => _dependencyProvider.GetDependency<ITestInterface>());
    }
}