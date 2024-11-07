using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class FieldSymbolExtensions
{
    #region GetPropertyName
    public static string GetPropertyName(this IFieldSymbol symbol) =>
        GetPropertyName(symbol.Name);
    
    public static string GetPropertyName(string fieldName)
    {
        var name = RemovePrefix(fieldName);
        return char.ToUpper(name[0]) + name.Substring(1);
    }
    #endregion

    #region RemovePrefix
    public static string RemovePrefix(this IFieldSymbol symbol) =>
        RemovePrefix(symbol.Name);

    public static string RemovePrefix(string fieldName)
    {
        var prefixCount = fieldName.StartsWith("_") ? 1 : fieldName.StartsWith("m_") ? 2 : 0;
        if (prefixCount > 0) fieldName = fieldName.Remove(0, prefixCount);

        return fieldName;
    }
    #endregion
}