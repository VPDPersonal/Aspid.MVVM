namespace MVVMGenerators.Helpers;

public class AttributeText(string name, NamespaceText? @namespace = null) : 
    TypeText(name + "Attribute", @namespace)
{
    public string AttributeName => name;

    public string AttributeFullName => (Namespace != null ? $"{Namespace}." : "") + AttributeName;
    
    public string AttributeGlobal => $"global::{AttributeFullName}";

    public override string ToString() =>
        AttributeGlobal;
}