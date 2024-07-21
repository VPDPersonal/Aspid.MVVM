using System.Linq;
using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class NamedSymbolExtensions
{
    public static bool HasInterface(this INamedTypeSymbol symbol, TypeText typeText) =>
        symbol.HasInterface(typeText.FullName);
    
    public static bool HasInterface(this INamedTypeSymbol symbol, string interfaceName) =>
        symbol.AllInterfaces.Any(@interface =>  @interface.ToDisplayString() == interfaceName);

    public static bool HasInterface(this INamedTypeSymbol symbol, TypeText typeText, out INamedTypeSymbol? foundInterface) =>
        symbol.HasInterface(typeText.FullName, out foundInterface);
    
    public static bool HasInterface(this INamedTypeSymbol symbol, string interfaceName, out INamedTypeSymbol? foundInterface)
    {
        foundInterface = null;
        
        foreach (var @interface in symbol.AllInterfaces)
        {
            if (@interface.ToDisplayString() != interfaceName) continue;
            
            foundInterface = @interface;
            return true;
        }

        return false;
    }

    public static bool HasBaseType(this INamedTypeSymbol symbol, TypeText typeText) =>
        symbol.HasBaseType(typeText.FullName);
    
    public static bool HasBaseType(this INamedTypeSymbol symbol, string baseTypeName)
    {
        for (var type = symbol; type != null; type = type.BaseType)
            if (type.ToDisplayString() == baseTypeName) return true;

        return false;
    }
    
    public static bool HasBaseType(this INamedTypeSymbol symbol, TypeText typeText, out INamedTypeSymbol? foundBaseType) =>
        symbol.HasBaseType(typeText.FullName, out foundBaseType);
    
    public static bool HasBaseType(this INamedTypeSymbol symbol, string baseTypeName, out INamedTypeSymbol? foundBaseType)
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
}