﻿using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;

namespace VideoClipExtractor.Tests.BaseUI.Services.DependencyInjection;

[UsedImplicitly]
public class NotEmptyImplementation : ITestInterface
{
    public NotEmptyImplementation(IDependencyProvider provider)
    {
    }
}