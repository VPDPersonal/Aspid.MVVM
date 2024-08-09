using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class PropertySymbolExtensions
{
    public static string GetFieldName(this IPropertySymbol symbol) =>
        GetFieldNameFromPropertyName(symbol.Name);
    
    public static string GetFieldNameFromPropertyName(string name)
    {
        if (name.Length == 0) return name;
        
        var firstSymbol = name[0];
        var isFirstSymbolUpper = char.IsUpper(firstSymbol);

        if (isFirstSymbolUpper)
        {
            name = name.Remove(0, 1);
            name = "_" + char.ToLower(firstSymbol) + name;
        }
        else
        {
            name = "_" + name;
        }

        return name;
    }
}