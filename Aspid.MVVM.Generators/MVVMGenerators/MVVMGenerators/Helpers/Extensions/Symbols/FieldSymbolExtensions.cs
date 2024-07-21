using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class FieldSymbolExtensions
{
    public static string GetPropertyName(this IFieldSymbol symbol) =>
        GetPropertyNameFromFieldName(symbol.Name);
    
    public static string GetPropertyNameFromFieldName(string name)
    {
        var prefixCount = GetPrefixCount();
        name = name.Remove(0, prefixCount);

        var firstSymbol = name[0];
        if (char.IsLower(firstSymbol))
        {
            name = name.Remove(0, 1);
            name = char.ToUpper(firstSymbol) + name;
        }
        
        return name;
        
        // TODO Custom prefix from config
        int GetPrefixCount() =>
            name.StartsWith("_") ? 1 : name.StartsWith("m_") ? 2 : 0;
    }
}