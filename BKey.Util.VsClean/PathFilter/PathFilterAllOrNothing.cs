namespace BKey.Util.VsClean.PathFilter;

[PathFilter("prompt")]
public class PathFilterAllOrNothing : IPathFilter
{
    public IEnumerable<string> FilterPaths(IEnumerable<string> paths)
    {
        Console.WriteLine("The following paths will be deleted:");
        foreach (var path in paths)
        {
            Console.WriteLine(path);
        }
        Console.WriteLine("Press Y to confirm deletion, any other key to cancel.");
        var confirmation = Console.ReadKey();
        if (confirmation.Key == ConsoleKey.Y)
        {
            return paths;
        }
        else
        {
            return Enumerable.Empty<string>();
        }
    }
}