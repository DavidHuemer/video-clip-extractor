namespace VideoClipExtractor.Tests.BaseUI.Services.Provider.DependencyInjection;

public class NotSuitableConstructor(int i)
{
    private readonly int _i = i;
}