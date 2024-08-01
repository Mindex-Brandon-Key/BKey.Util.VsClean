namespace BKey.Util.VsClean.Discovery;
internal class FolderHelper
{
    public static List<string> FindFolders(string root, string[] folderNames)
    {
        var folders = new List<string>();
        var directories = new Stack<string>();
        directories.Push(root);

        while (directories.Count > 0)
        {
            var currentDir = directories.Pop();
            if (Path.GetFileName(currentDir) == ".git")
            {
                continue;
            }

            try
            {
                foreach (var dir in Directory.GetDirectories(currentDir))
                {
                    if (!folderNames.Contains(Path.GetFileName(dir)) && Path.GetFileName(dir) != ".git")
                    {
                        directories.Push(dir);
                    }
                    else if (folderNames.Contains(Path.GetFileName(dir)))
                    {
                        folders.Add(dir);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to access directory: {currentDir}. Error: {ex.Message}");
            }
        }

        return folders;
    }
}
