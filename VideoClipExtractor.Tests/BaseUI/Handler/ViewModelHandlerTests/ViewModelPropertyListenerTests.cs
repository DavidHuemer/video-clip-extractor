using BaseUI.Handler.ViewModelHandler;

namespace VideoClipExtractor.Tests.BaseUI.Handler.ViewModelHandlerTests;

public class ViewModelPropertyListenerTests
{
    private ExampleViewModel _viewModel = null!;
    private ViewModelPropertyListener _viewModelPropertyListener = null!;

    [SetUp]
    public void Setup()
    {
        _viewModel = new ExampleViewModel();
        _viewModelPropertyListener = new ViewModelPropertyListener(_viewModel);
    }

    [Test]
    public void AddPropertyListener_WhenCalled_AddsListenerToViewModel()
    {
        // Arrange
        var propertyValue = "Test";
        var result = "";

        var callBack = new Action<string?>(s => { result = s; });

        // Act
        _viewModelPropertyListener.AddPropertyListener(nameof(ExampleViewModel.ExampleProperty), callBack);

        _viewModel.ExampleProperty = propertyValue;

        Assert.That(result, Is.EqualTo(propertyValue));
    }

    [Test]
    public void AddPropertyListener_ToNullableProperty()
    {
        string? result = "";
        var callBack = new Action<string?>(s => { result = s; });

        _viewModelPropertyListener.AddPropertyListener(nameof(ExampleViewModel.NullAble), callBack);
        _viewModel.NullAble = null;

        Assert.That(result, Is.Null);
    }

    [Test]
    public void AddPropertyListener_WhenPropertyDoesNotNotify_DoesNotAddListenerToViewModel()
    {
        // Arrange
        var propertyValue = "Test";
        var result = "";

        var callBack = new Action<string?>(s => { result = s; });

        // Act
        _viewModelPropertyListener.AddPropertyListener(nameof(ExampleViewModel.NotNotify), callBack);

        _viewModel.ExampleProperty = propertyValue;

        Assert.That(result, Is.Not.EqualTo(propertyValue));
    }

    [Test]
    public void AddPropertyListener_ToNotExistingProperty_DoesNotAddListenerToViewModel()
    {
        // Arrange
        var propertyValue = "Test";
        var result = "";

        var callBack = new Action<string?>(s => { result = s; });

        // Act
        _viewModelPropertyListener.AddPropertyListener("NotExistingProperty", callBack);

        _viewModel.ExampleProperty = propertyValue;

        Assert.That(result, Is.Not.EqualTo(propertyValue));
    }
}