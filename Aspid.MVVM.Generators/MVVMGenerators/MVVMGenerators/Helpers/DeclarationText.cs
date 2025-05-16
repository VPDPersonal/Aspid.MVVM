using System.Linq;

namespace MVVMGenerators.Helpers;

public readonly struct DeclarationText(string? modifiers, string typeType, string name, string? genericArguments)
{
    public string? Modifiers { get; } = modifiers;

    public string TypeType { get; } = typeType;

    public string Name { get; } = name;

    public string? GenericArguments { get; } = genericArguments;

    public string GetFileName(string? namespaceName, string? postfix)
    {
        postfix ??= "";
        if (postfix.Length > 0 && postfix[0] != '.') postfix = $".{postfix}";

        namespaceName = string.IsNullOrEmpty(namespaceName) ? "" : $"{namespaceName}.";
        
        return namespaceName + (string.IsNullOrEmpty(GenericArguments)
            ? $"{Name}{postfix}.g.cs" 
            : $"{Name}`{GenericArguments!.Count(arg => arg == ',') + 1}{postfix}.g.cs");
    }
    
    public override string ToString()
    {
        var modifiers = !string.IsNullOrEmpty(Modifiers) ?  $"{Modifiers} " : "";
        var arguments = !string.IsNullOrEmpty(GenericArguments) ? $"<{GenericArguments}>" : "";

        return $"{modifiers}{TypeType} {Name}{arguments}";
    }
    
    public static implicit operator string(DeclarationText declaration) =>
        declaration.ToString();
}