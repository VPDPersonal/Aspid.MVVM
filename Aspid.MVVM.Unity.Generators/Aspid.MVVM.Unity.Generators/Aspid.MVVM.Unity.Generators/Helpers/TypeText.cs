namespace Aspid.Generator.Helpers;

public class TypeText(string name, NamespaceText? @namespace = null)
{
    public string Name { get; } = name;

    public NamespaceText? Namespace { get; } = @namespace;

    public string FullName => (Namespace != null ? $"{Namespace}." : "") + Name;
    
    public string Global => $"global::{FullName}";

    public override string ToString() =>
        Global;
    
    public static implicit operator string(TypeText? type) =>
        type.ToString();
}