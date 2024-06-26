﻿namespace BaseUI.Services.Provider.DependencyInjection;

/// <summary>
///     Provides access to dependencies
/// </summary>
public interface IDependencyProvider
{
    /// <summary>
    ///     Adds a dependency to the provider
    /// </summary>
    /// <typeparam name="TInterface">The interface of the dependency that should be provided</typeparam>
    /// <typeparam name="TImplementation">The implementation of the dependency that should be provided</typeparam>
    void AddSingletonDependency<TInterface, TImplementation>() where TImplementation : class, TInterface;

    void AddTransientDependency<TInterface, TImplementation>() where TImplementation : TInterface;

    /// <summary>
    ///     Returns a dependency
    /// </summary>
    /// <typeparam name="TInterface">The interface of the dependency that should be provided</typeparam>
    /// <returns>The provided dependency</returns>
    TInterface GetDependency<TInterface>() where TInterface : class;
}