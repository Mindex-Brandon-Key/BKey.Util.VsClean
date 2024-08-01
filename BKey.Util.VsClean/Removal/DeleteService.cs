namespace BKey.Util.VsClean.Removal;
public class DeleteService : IDeleteService
{
    public void RemovePaths(IEnumerable<string> paths)
    {
        foreach (var path in paths)
        {
            RemoveFolder(path);
        }
    }

    private void RemoveFolder(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            try
            {
                Directory.Delete(folderPath, true);
                Console.WriteLine($"Removed folder: {folderPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to remove folder: {folderPath}. Error: {ex.Message}");
            }
        }
    }
}
