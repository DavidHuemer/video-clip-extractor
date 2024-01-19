namespace BaseUI.Services.FileServices.Implementations;

/// <summary>
///     Basic implementation of the <see cref="IFileExplorer" /> interface
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class FileExplorer : IFileExplorer
{
    public string GetSaveFilePath(string filter)
    {
        using var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = filter;
        var result = saveFileDialog.ShowDialog();
        return result == DialogResult.OK ? saveFileDialog.FileName : "";
    }
}