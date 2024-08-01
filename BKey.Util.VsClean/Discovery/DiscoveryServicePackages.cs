namespace BKey.Util.VsClean.Discovery;

[DiscoveryService("packages")]
public class DiscoveryServicePackages : IPathDiscoveryService
{
    public IEnumerable<string> CollectPaths(string root)
    {
        return FolderHelper.FindFolders(root, new[] { "packages" });
    }
}