using System.Runtime.CompilerServices;

namespace BaseUI.Exceptions.Basics;

public class NotSetupException(string clazz, string setupMethod, [CallerMemberName] string? caller = null)
    : Exception($"The class {clazz} has not been setup. Please call {setupMethod} before calling {caller}");