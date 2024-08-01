using BKey.Util.VsClean.Discovery;
using BKey.Util.VsClean.PathFilter;
using BKey.Util.VsClean.Removal;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace BKey.Util.VsClean;

internal class Program
{
    static IDiscoveryServiceFactory DiscoveryServiceFactory { get; } = new DiscoveryServiceFactory();
    static IPathFilterFactory PathFilterFactory { get; } = new PathFilterFactory();

    static int Main(string[] args)
    {
        var rootCommand = new RootCommand("Deep Clean a Visual Studio Project")
            {
                new Option<string[]>(
                    new string[] { "--remove", "-r" },
                    "List of folder types to remove: bin-obj, vs, packages")
                {
                    IsRequired = true,
                }
                .FromAmong(DiscoveryServiceFactory.ListServices().ToArray()),
                new Option<string>(
                    new string[] { "--mode", "-m" },
                    () => "prompt",
                    "Mode for accepting or denying deletion")
                .FromAmong(PathFilterFactory.ListServices().ToArray()),
                new Option<bool>(
                    "--dry-run",
                    "List the files and folders to be deleted without actually deleting them"),
                new Argument<string[]>(
                    "paths",
                    "Paths to folders or csproj/sln files")
                {
                    Arity = ArgumentArity.OneOrMore,
                }
            };

        rootCommand.Handler = CommandHandler.Create<string[], string, bool, string[]>((remove, mode, dryRun, paths) =>
        {
            var discoveryServices = GetDiscoveryServices(remove);
            var pathFilter = PathFilterFactory.CreateService(mode);
            var deleteService = dryRun ? (IDeleteService)new DeleteServiceDryRun() : new DeleteService();

            var allPathsToRemove = new HashSet<string>();

            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    var directory = Path.GetDirectoryName(path);
                    CollectPaths(discoveryServices, directory, allPathsToRemove);
                }
                else if (Directory.Exists(path))
                {
                    CollectPaths(discoveryServices, path, allPathsToRemove);
                }
                else
                {
                    Console.WriteLine($"Invalid path: {path}");
                }
            }

            var filteredPaths = pathFilter.FilterPaths(allPathsToRemove);
            deleteService.RemovePaths(filteredPaths);
        });

        return rootCommand.InvokeAsync(args).Result;
    }

    static void CollectPaths(IEnumerable<IPathDiscoveryService> services, string root, ISet<string> allPathsToRemove)
    {
        foreach (var service in services)
        {
            var paths = service.CollectPaths(root);
            foreach (var path in paths)
            {
                allPathsToRemove.Add(path);
            }
        }
    }

    static IEnumerable<IPathDiscoveryService> GetDiscoveryServices(string[] removes)
    {
        foreach (var remove in removes)
        {
            var service = DiscoveryServiceFactory.CreateService(remove)
                ?? throw new ArgumentException($"Unknown removal option '{remove}'", nameof(removes));
            yield return service;
        }
    }
}
