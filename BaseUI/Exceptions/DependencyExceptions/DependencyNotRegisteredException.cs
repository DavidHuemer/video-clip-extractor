namespace BaseUI.Exceptions.DependencyExceptions;

public class DependencyNotRegisteredException : Exception
{
    public DependencyNotRegisteredException(Type type) : base($"Type {type} is not registered")
    {
    }
}