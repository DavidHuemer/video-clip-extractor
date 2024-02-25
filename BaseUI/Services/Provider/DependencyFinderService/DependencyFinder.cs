using System.Reflection;
using BaseUI.Services.Provider.Attributes;

namespace BaseUI.Services.Provider.DependencyFinderService;

[Service]
[Singleton]
public class DependencyFinder : IDependencyFinder
{
    public Type? FindDependency<TInterface>() where TInterface : class
    {
        var assemblies = GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                // continue if not instance of TInterface or not assignable to TInterface
                if (!type.IsAssignableTo(typeof(TInterface)) || type.IsInterface) continue;

                return type;
            }
        }

        return null;
    }

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