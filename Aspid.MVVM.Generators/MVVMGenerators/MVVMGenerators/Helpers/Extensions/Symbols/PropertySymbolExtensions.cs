using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class PropertySymbolExtensions
{
    public static string GetFieldName(this IPropertySymbol symbol, bool hasPrefix = true) =>
        GetFieldName(symbol.Name, hasPrefix);
    
    public static string GetFieldName(string name, bool hasPrefix = true)
    {
        if (name.Length == 0) return name;
        
        var firstSymbol = name[0];
        var isFirstSymbolUpper = char.IsUpper(firstSymbol);

        if (isFirstSymbolUpper)
        {
            name = name.Remove(0, 1);
            name = char.ToLower(firstSymbol) + name;
        }
        
        if (hasPrefix)
        {
            name = "_" + name;
        }
        
        return name;
    }
}