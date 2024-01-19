namespace BaseUI.Data;

/// <summary>
///     Contains properties for a file type.
/// </summary>
/// <param name="Name">The name of the file type</param>
/// <param name="Extension">The extension of the file type</param>
public record FileTypeInfo(string Name, string Extension)
{
    public string FileFilter => $"{Name} (*.{Extension})|*.{Extension}";
}