namespace BKey.Util.VsClean.Discovery;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class DiscoveryServiceAttribute : Attribute
{
    public string Name { get; }

    public DiscoveryServiceAttribute(string name)
    {
        Name = name;
    }
}
