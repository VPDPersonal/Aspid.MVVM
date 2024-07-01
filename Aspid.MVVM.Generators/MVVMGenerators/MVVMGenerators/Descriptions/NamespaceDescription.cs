namespace MVVMGenerators.Descriptions;

public sealed class NamespaceDescription(string name, NamespaceDescription? parent = null)
{
    public string Name { get; } = parent ?? "" + name;

    public override string ToString() => Name;

    public static implicit operator string(NamespaceDescription @namespace) =>
        @namespace.ToString();
}