using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.Provider.DependencyFinderService;
using BaseUI.Services.Provider.DependencyInjection;
using Moq;

namespace VideoClipExtractor.Tests.BaseUI.Services.DependencyInjection;

public class DependencyProviderTests
{
    private Mock<IDependencyFinder> _dependencyFinder = null!;
    private Mock<IDependencyInstanceBuilder> _dependencyInstanceBuilder = null!;
    private DependencyProvider _dependencyProvider = null!;

    // Before each test
    [SetUp]
    public void Setup()
    {
        _dependencyInstanceBuilder = new Mock<IDependencyInstanceBuilder>();
        _dependencyFinder = new Mock<IDependencyFinder>();
        _dependencyProvider = new DependencyProvider(_dependencyInstanceBuilder.Object, _dependencyFinder.Object);
    }

    [Test]
    public void ReturnNewInstanceOfTransientDependency()
    {
        // Arrange
        _dependencyProvider.AddTransientDependency<ITestInterface, TestImplementation>();
        _dependencyInstanceBuilder.Setup(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()))
            .Returns(new TestImplementation());

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsInstanceOf<TestImplementation>(testInterface);
            _dependencyInstanceBuilder.Verify(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()), Times.Once);
        });
    }

    [Test]
    public void TransientDependencyIsUpdated()
    {
        // Arrange
        _dependencyProvider.AddTransientDependency<ITestInterface, TestImplementation>();
        _dependencyProvider.AddTransientDependency<ITestInterface, SecondImplementation>();

        _dependencyInstanceBuilder.Setup(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()))
            .Returns(new SecondImplementation());

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsInstanceOf<SecondImplementation>(testInterface);
            _dependencyInstanceBuilder.Verify(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()), Times.Once);
        });
    }

    [Test]
    public void TransientDependencyIsAlwaysNewInstance()
    {
        // Arrange
        _dependencyProvider.AddTransientDependency<ITestInterface, TestImplementation>();
        _dependencyInstanceBuilder.Setup(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()))
            .Returns(new TestImplementation());

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();
        var testInterface2 = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsInstanceOf<TestImplementation>(testInterface);
            Assert.IsInstanceOf<TestImplementation>(testInterface2);
            _dependencyInstanceBuilder.Verify(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()),
                Times.Exactly(2));
        });
    }

    [Test]
    public void ReturnSameInstanceOfSingletonDependency()
    {
        // Arrange
        _dependencyProvider.AddSingletonDependency<ITestInterface, TestImplementation>();
        _dependencyInstanceBuilder.Setup(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()))
            .Returns(new TestImplementation());

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();
        var testInterface2 = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsInstanceOf<TestImplementation>(testInterface);
            Assert.IsInstanceOf<TestImplementation>(testInterface2);
            Assert.That(testInterface2, Is.SameAs(testInterface));
            _dependencyInstanceBuilder.Verify(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()), Times.Once);
        });
    }

    [Test]
    public void SingletonDependencyIsUpdated()
    {
        // Arrange
        _dependencyProvider.AddSingletonDependency<ITestInterface, TestImplementation>();
        _dependencyProvider.AddSingletonDependency<ITestInterface, SecondImplementation>();
        _dependencyInstanceBuilder.Setup(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()))
            .Returns(new SecondImplementation());

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsInstanceOf<SecondImplementation>(testInterface);
            _dependencyInstanceBuilder.Verify(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()), Times.Once);
        });
    }

    [Test]
    public void ThrowDependencyNotRegisteredException()
    {
        // Assert
        Assert.Throws<DependencyNotRegisteredException>(() => _dependencyProvider.GetDependency<ITestInterface>());
    }

    [Test]
    public void SingletonDependencyAttributeReturnsSameInstance()
    {
        _dependencyFinder.Setup(x => x.FindDependency<ITestInterface>()).Returns(typeof(SingletonImplementation));
        _dependencyInstanceBuilder.Setup(x => x.InstantiateType<ITestInterface>(It.IsAny<Type>()))
            .Returns(new SingletonImplementation());

        // Act
        var testInterface = _dependencyProvider.GetDependency<ITestInterface>();
        var testInterface2 = _dependencyProvider.GetDependency<ITestInterface>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsInstanceOf<SingletonImplementation>(testInterface);
            Assert.IsInstanceOf<SingletonImplementation>(testInterface2);
            Assert.That(testInterface2, Is.SameAs(testInterface));
        });
    }
}