namespace MVVMGenerators.Helpers;

public readonly struct DeclarationText(string? modifiers, string typeType, string name, string? genericArguments)
{
    public string? Modifiers { get; } = modifiers;

    public string TypeType { get; } = typeType;

    public string Name { get; } = name;

    public string? GenericArguments { get; } = genericArguments;

    public string GetFileName(string? postfix)
    {
        postfix ??= "";
        if (postfix.Length > 0 && postfix[0] != '.') postfix = $".{postfix}";
        
        return string.IsNullOrEmpty(GenericArguments)
            ? $"{Name}{postfix}.cs" 
            : $"{Name}{{{GenericArguments}}}{postfix}.cs";
    }
    
    public override string ToString()
    {
        var modifiers = !string.IsNullOrEmpty(Modifiers) ?  $"{Modifiers} " : "";
        var arguments = !string.IsNullOrEmpty(GenericArguments) ? $"<{GenericArguments}>" : "";

        return $"{modifiers}{TypeType} {Name}{arguments}";
    }
}