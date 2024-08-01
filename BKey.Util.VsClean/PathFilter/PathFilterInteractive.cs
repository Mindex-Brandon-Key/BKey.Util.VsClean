namespace BKey.Util.VsClean.PathFilter;
[PathFilter("interactive")]
public class PathFilterInteractive : IPathFilter
{
    public IEnumerable<string> FilterPaths(IEnumerable<string> paths)
    {
        var acceptedPaths = new List<string>();
        foreach (var path in paths)
        {
            Console.WriteLine($"Delete {path}? (y/n)");
            var key = Console.ReadKey();
            Console.WriteLine();
            if (key.Key == ConsoleKey.Y)
            {
                acceptedPaths.Add(path);
            }
        }
        return acceptedPaths;
    }
}

