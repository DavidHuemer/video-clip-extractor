using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.Provider.DependencyFinderService;
using BaseUI.Services.Provider.DependencyInjection;
using Moq;

namespace VideoClipExtractor.Tests.BaseUI.Services.DependencyInjection;

public class DependencyProviderTests
{
    private Mock<IDependencyFinder> _dependencyFinder = null!;
    private DependencyProvider _dependencyProvider = null!;

    // Before each test
    [SetUp]
    public void Setup()
    {
        _dependencyFinder = new Mock<IDependencyFinder>();
        _dependencyProvider = new DependencyProvider(_dependencyFinder.Object);
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
    public void TransientDependencyIsAlwaysNewInstance()
    {
        // Arrange
        _dependencyProvider.AddTransientDependency<ITestInterface, TestImplementation>();

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();
        var testInterface2 = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.IsInstanceOf<TestImplementation>(testInterface);
        Assert.IsInstanceOf<TestImplementation>(testInterface2);
        Assert.That(testInterface2, Is.Not.SameAs(testInterface));
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
        Assert.Throws<DependencyNotRegisteredException>(() => _dependencyProvider.GetDependency<ITestInterface>());
    }

    [Test]
    public void InstantiatesInstanceOfNotEmptyConstructor()
    {
        _dependencyProvider.AddTransientDependency<ITestInterface, NotEmptyImplementation>();

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.IsInstanceOf<NotEmptyImplementation>(testInterface);
    }
    
    [Test]
    public void SingletonDependencyAttributeReturnsSameInstance()
    {
        _dependencyFinder.Setup(x => x.FindDependency<ITestInterface>()).Returns(typeof(SingletonImplementation));
        
        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();
        var testInterface2 = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.IsInstanceOf<SingletonImplementation>(testInterface);
        Assert.IsInstanceOf<SingletonImplementation>(testInterface2);
        Assert.That(testInterface2, Is.SameAs(testInterface));
    }
}