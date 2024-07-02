namespace MVVMGenerators.Helpers;

public sealed class NamespaceText(string name, NamespaceText? parent = null)
{
    public string Name { get; } = (parent != null ?  $"{parent}." : "") + name;

    public override string ToString() => Name;

    public static implicit operator string(NamespaceText @namespace) =>
        @namespace.ToString();
}