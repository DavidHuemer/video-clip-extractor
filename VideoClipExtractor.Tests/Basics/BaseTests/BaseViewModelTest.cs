using Moq;
using VideoClipExtractor.Tests.Basics.Mocks;

namespace VideoClipExtractor.Tests.Basics.BaseTests;

public class BaseViewModelTest : BaseDependencyTest
{
    #region Protected Fields

    protected ViewModelProviderMock ViewModelProviderMock = null!;

    #endregion

    public override void Setup()
    {
        base.Setup();
        ViewModelProviderMock = new ViewModelProviderMock();
        AddMockDependency(ViewModelProviderMock);
    }

    protected void AddViewModel<T>(Mock<T> mock) where T : class => ViewModelProviderMock.AddViewModel(mock);
}