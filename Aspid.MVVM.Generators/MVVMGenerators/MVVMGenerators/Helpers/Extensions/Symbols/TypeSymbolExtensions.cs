using System.Linq;
using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class TypeSymbolExtensions
{
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
}