namespace BKey.Util.VsClean.PathFilter;

[PathFilter("skip-prompt")]
public class PathFilterAll : IPathFilter
{
    public IEnumerable<string> FilterPaths(IEnumerable<string> paths)
    {
        Console.WriteLine("The following paths will be deleted:");
        foreach (var path in paths)
        {
            Console.WriteLine(path);
        }
        return paths;
    }
}