namespace MVVMGenerators.Helpers;

public readonly struct DeclarationText(string? modifiers, string typeType, string name, string? genericArguments)
{
    public string? Modifiers { get; } = modifiers;

    public string TypeType { get; } = typeType;

    public string Name { get; } = name;

    public string? GenericArguments { get; } = genericArguments;

    public override string ToString()
    {
        var modifiers = !string.IsNullOrEmpty(Modifiers) ?  $"{Modifiers} " : "";
        var genericArguments = !string.IsNullOrEmpty(GenericArguments) ? $"<{GenericArguments}>" : "";

        return $"{modifiers}{TypeType} {Name}{genericArguments}";
    }
}