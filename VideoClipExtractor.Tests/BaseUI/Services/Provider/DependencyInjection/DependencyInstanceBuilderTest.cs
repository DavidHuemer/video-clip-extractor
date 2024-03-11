using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.InstanceBuilderService;
using Moq;
using VideoClipExtractor.Tests.BaseUI.Services.Provider.InstanceBuilderTests;
using VideoClipExtractor.Tests.Basics.BaseTests;

namespace VideoClipExtractor.Tests.BaseUI.Services.Provider.DependencyInjection;

[TestFixture]
[TestOf(typeof(DependencyInstanceBuilder))]
public class DependencyInstanceBuilderTest : BaseDependencyTest
{
    private Mock<IInstanceBuilderService> _instanceBuilderService = null!;
    private DependencyInstanceBuilder _dependencyInstanceBuilder = null!;

    public override void Setup()
    {
        base.Setup();
        _instanceBuilderService = new Mock<IInstanceBuilderService>();
        _dependencyInstanceBuilder =
            new DependencyInstanceBuilder(DependencyMock.Object, _instanceBuilderService.Object);
    }

    [Test]
    public void InstantiateTypeUsesParameterless()
    {
        _instanceBuilderService.Setup(x => x.InstantiateType<TestInstance>(It.IsAny<Type>()))
            .Returns(new TestInstance());
        var instance = _dependencyInstanceBuilder.InstantiateType<TestInstance>(typeof(TestInstance));

        Assert.Multiple(() =>
        {
            Assert.IsNotNull(instance);
            Assert.IsInstanceOf<TestInstance>(instance);
            _instanceBuilderService.Verify(x => x.InstantiateType<TestInstance>(It.IsAny<Type>()), Times.Once);
        });
    }

    [Test]
    public void InstantiateTypeUsesProviderParameter()
    {
        var instance = new DependencyInstance(DependencyMock.Object);
        _instanceBuilderService
            .Setup(x => x.InstantiateType<DependencyInstance>(It.IsAny<Type>(), It.IsAny<object[]>()))
            .Returns(instance);

        var result = _dependencyInstanceBuilder.InstantiateType<DependencyInstance>(typeof(DependencyInstance));

        Assert.Multiple(() =>
        {
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<DependencyInstance>(result);
            _instanceBuilderService.Verify(
                x => x.InstantiateType<DependencyInstance>(It.IsAny<Type>(), It.IsAny<object[]>()), Times.Once);
        });
    }

    [Test]
    public void NoSuitableConstructorThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() =>
            _dependencyInstanceBuilder.InstantiateType<NotSuitableConstructor>(typeof(NotSuitableConstructor)));
    }
}