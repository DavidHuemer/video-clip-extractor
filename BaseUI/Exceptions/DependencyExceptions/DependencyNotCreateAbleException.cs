namespace BaseUI.Exceptions.DependencyExceptions;

public class DependencyNotCreateAbleException : Exception
{
    public DependencyNotCreateAbleException(Type type) : base($"Type {type} is not create able")
    {
    }
}