namespace BKey.Util.VsClean.Discovery;
[DiscoveryService(".vs")]
public class DiscoveryServiceVs : IPathDiscoveryService
{
    public IEnumerable<string> CollectPaths(string root)
    {
        return FolderHelper.FindFolders(root, new[] { ".vs" });
    }
}