using System.Linq;
using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static partial class TypeSymbolExtensions
{
    public static bool HasBaseType(this ITypeSymbol symbol, TypeText typeText) =>
        HasBaseType(symbol, typeText.FullName);

    public static bool HasBaseType(this ITypeSymbol symbol, string baseTypeName) =>
        HasBaseType(symbol, baseTypeName, out _);
    
    public static bool HasBaseType(this ITypeSymbol symbol, TypeText typeText, out ITypeSymbol? foundBaseType) =>
        HasBaseType(symbol, typeText.FullName, out foundBaseType);
    
    public static bool HasBaseType(this ITypeSymbol symbol, string baseTypeName, out ITypeSymbol? foundBaseType)
    {
        foundBaseType = null;
        
        for (var type = symbol; type != null; type = type.BaseType)
        {
            if (type.ToDisplayString() != baseTypeName) continue;
            
            foundBaseType = type;
            return true;
        }
        
        return false;
    }

    public static bool HasBaseType(this ITypeSymbol symbol, params TypeText[] baseTypeNames) =>
        HasBaseType(symbol, baseTypeNames.Select(baseTypeName => baseTypeName.FullName).ToArray());

    public static bool HasBaseType(this ITypeSymbol symbol, params string[] baseTypeNames)
    {
        for (var type = symbol; type != null; type = type.BaseType)
        {
            if (baseTypeNames.Any(baseTypeName => type.ToDisplayString() == baseTypeName))
            {
                return true;
            }
        }
        
        return false;
    }
}