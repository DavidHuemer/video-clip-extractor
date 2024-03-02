using System.Reflection;
using BaseUI.Services.Provider.Attributes;

namespace BaseUI.Services.Provider.DependencyFinderService;

[Service]
[Transient]
public class DependencyFinder : IDependencyFinder
{
    public Predicate<Type> TypePredicate { get; set; } = _ => true;

    public Type? FindDependency<TInterface>() where TInterface : class => GetAssemblies()
        .SelectMany(assembly => assembly.GetTypes())
        .FirstOrDefault(type => type.IsAssignableTo(typeof(TInterface)) && !type.IsInterface && TypePredicate(type));

    private static Assembly[] GetAssemblies() =>
        AppDomain.CurrentDomain.GetAssemblies().Where(IsAssemblySuitable).ToArray();

    private static bool IsAssemblySuitable(Assembly assembly) => assembly.FullName != null &&
                                                                 !assembly.FullName.StartsWith("System") &&
                                                                 !assembly.FullName.StartsWith("Microsoft") &&
                                                                 !assembly.FullName.StartsWith("ReSharper") &&
                                                                 !assembly.FullName.StartsWith("netstandard") &&
                                                                 !assembly.FullName.StartsWith("nunit") &&
                                                                 !assembly.FullName.StartsWith("Moq") &&
                                                                 !assembly.FullName.StartsWith("testcentric") &&
                                                                 !assembly.FullName.StartsWith("Castle.Core") &&
                                                                 !assembly.FullName.StartsWith("JetBrains");
}