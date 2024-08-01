namespace BKey.Util.VsClean.Discovery;
[DiscoveryService("bin-obj")]
public class DiscoveryServiceBinObj : IPathDiscoveryService
{
    public IEnumerable<string> CollectPaths(string root)
    {
        return FolderHelper.FindFolders(root, new[] { "bin", "obj" });
    }
}
