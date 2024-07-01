namespace MVVMGenerators.Descriptions;

public sealed class TypeDescription(string name, NamespaceDescription? @namespace = null)
{
    public string Name { get; } = name;

    public NamespaceDescription? Namespace { get; } = @namespace;

    public string FullName => Namespace ?? "" + Name;
}