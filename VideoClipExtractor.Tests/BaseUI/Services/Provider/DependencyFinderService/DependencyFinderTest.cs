using BaseUI.Services.Provider.DependencyFinderService;

namespace VideoClipExtractor.Tests.BaseUI.Services.Provider.DependencyFinderService;

[TestFixture]
[TestOf(typeof(DependencyFinder))]
public class DependencyFinderTest
{
    [SetUp]
    public void SetUp()
    {
        _dependencyFinder = new DependencyFinder();
    }

    private DependencyFinder _dependencyFinder = null!;

    [Test]
    public void FindDependencyWithNotImplementedDependencyReturnsNull()
    {
        var result = _dependencyFinder.FindDependency<INotExistingDependency>();
        Assert.That(result, Is.Null);
    }

    [Test]
    public void FindDependencyWithTypePredicateReturnsNull()
    {
        var result = _dependencyFinder.FindDependency<IExistingDependency>();
        Assert.That(result, Is.Not.Null);

        _dependencyFinder.TypePredicate = type => type != typeof(ExistingDependency);
        result = _dependencyFinder.FindDependency<IExistingDependency>();
        Assert.That(result, Is.Null);
    }
}