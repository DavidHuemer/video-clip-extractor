namespace BaseUI.Data;

/// <summary>
///     Contains information about a recently opened file.
/// </summary>
public class RecentlyOpenedFileInfo
{
    /// <summary>
    ///     The path to the recently opened file
    /// </summary>
    public required string Path { get; set; }

    /// <summary>
    ///     The time the file was last opened
    /// </summary>
    public required DateTime LastOpened { get; set; }
}