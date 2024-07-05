using System.Linq;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;

namespace MVVMGenerators.Extensions.Symbols;

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
}