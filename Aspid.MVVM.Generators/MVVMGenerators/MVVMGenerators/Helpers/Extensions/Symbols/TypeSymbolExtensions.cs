using System.Linq;
using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class TypeSymbolExtensions
{
    public static string ToDisplayStringGlobal(this ITypeSymbol symbol) =>
        symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    
    public static bool HaseDirectInterface(this ITypeSymbol type, TypeText typeText) =>
        type.HaseDirectInterface(typeText.FullName);

    public static bool HaseDirectInterface(this ITypeSymbol type, string interfaceName) =>
        type.HaseDirectInterface(interfaceName, out _);

    public static bool HaseDirectInterface(this ITypeSymbol type, TypeText typeText, out INamedTypeSymbol? foundInterface) =>
        type.HaseDirectInterface(typeText.FullName, out foundInterface);

    public static bool HaseDirectInterface(this ITypeSymbol type, string interfaceName, out INamedTypeSymbol? foundInterface)
    {
        foundInterface = null;
        
        foreach (var @interface in type.Interfaces)
        {
            if (@interface.ToDisplayString() != interfaceName) continue;
            
            foundInterface = @interface;
            return true;
        }

        return false;
    }
    
    public static bool HasInterface(this ITypeSymbol type, TypeText typeText) =>
        type.HasInterface(typeText.FullName);
    
    public static bool HasInterface(this ITypeSymbol type, string interfaceName) =>
        type.AllInterfaces.Any(@interface =>  @interface.ToDisplayString() == interfaceName);

    public static bool HasInterface(this ITypeSymbol type, TypeText typeText, out INamedTypeSymbol? foundInterface) =>
        type.HasInterface(typeText.FullName, out foundInterface);
    
    public static bool HasInterface(this ITypeSymbol type, string interfaceName, out INamedTypeSymbol? foundInterface)
    {
        foundInterface = null;
        
        foreach (var @interface in type.AllInterfaces)
        {
            if (@interface.ToDisplayString() != interfaceName) continue;
            
            foundInterface = @interface;
            return true;
        }

        return false;
    }
    
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