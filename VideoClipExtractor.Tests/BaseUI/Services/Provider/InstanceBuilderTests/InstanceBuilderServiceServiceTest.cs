using BaseUI.Services.Provider.InstanceBuilderService;

namespace VideoClipExtractor.Tests.BaseUI.Services.Provider.InstanceBuilderTests;

[TestFixture]
[TestOf(typeof(InstanceBuilderService))]
public class InstanceBuilderServiceServiceTest
{
    [SetUp]
    public void Setup()
    {
        _instanceBuilderServiceService = new InstanceBuilderService();
    }

    private InstanceBuilderService _instanceBuilderServiceService = null!;

    [Test]
    public void InstanceIsCreated()
    {
        var instance = _instanceBuilderServiceService.InstantiateType<TestInstance>(typeof(TestInstance));
        Assert.That(instance, Is.Not.Null);
    }

    [Test]
    public void InstanceIsCreatedForNotEmptyConstructor()
    {
        var instance =
            _instanceBuilderServiceService.InstantiateType<TestInstanceNotEmpty>(typeof(TestInstanceNotEmpty), [18]);

        Assert.That(instance.Number, Is.EqualTo(18));
    }

    [Test]
    public void InstanceNotEmptyConstructorThrowsError()
    {
        Assert.Throws<InvalidOperationException>(() =>
            _instanceBuilderServiceService.InstantiateType<TestInstanceNotEmpty>(typeof(TestInstance)));
    }
}