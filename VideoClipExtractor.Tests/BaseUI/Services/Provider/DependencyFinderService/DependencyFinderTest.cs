using BaseUI.Services.Provider.DependencyFinderService;

namespace VideoClipExtractor.Tests.BaseUI.Services.Provider.DependencyFinderService;

[TestFixture]
[TestOf(typeof(DependencyFinder))]
public class DependencyFinderTest
{
    private DependencyFinder _dependencyFinder = null!;

    [SetUp]
    public void SetUp()
    {
        _dependencyFinder = new DependencyFinder();
    }

    [Test]
    public void FindDependencyWithNotImplementedDependencyReturnsNull()
    {
        var result = _dependencyFinder.FindDependency<INotExistingDependency>();
        Assert.That(result, Is.Null);
    }
}