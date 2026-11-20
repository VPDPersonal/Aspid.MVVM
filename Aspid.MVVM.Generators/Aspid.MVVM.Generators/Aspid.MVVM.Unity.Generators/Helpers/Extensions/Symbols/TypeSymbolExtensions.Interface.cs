using System.Linq;
using Microsoft.CodeAnalysis;

namespace Aspid.Generator.Helpers;

public static partial class TypeSymbolExtensions
{
    public static bool HasInterface(this ITypeSymbol type, TypeText typeText) =>
        type.HasInterface(typeText.FullName);

    public static bool HasInterface(this ITypeSymbol type, string name) =>
        type.HasInterface(name, out _);

    public static bool HasInterface(this ITypeSymbol type, TypeText typeText, out INamedTypeSymbol? foundInterface) =>
        type.HasInterface(typeText.FullName, out foundInterface);

    public static bool HasInterface(this ITypeSymbol type, string name, out INamedTypeSymbol? foundInterface)
    {
        foundInterface = null;
        
        foreach (var @interface in type.Interfaces)
        {
            if (@interface.ToDisplayString() != name) continue;
            
            foundInterface = @interface;
            return true;
        }

        return false;
    }

    public static bool HasInterfaceInBases(this ITypeSymbol type, TypeText typeText) =>
        type.HasInterfaceInBases(typeText.FullName);

    public static bool HasInterfaceInBases(this ITypeSymbol type, string name) =>
        type.HasInterfaceInBases(name, out _);

    public static bool HasInterfaceInBases(this ITypeSymbol type, TypeText typeText, out INamedTypeSymbol? foundInterface) =>
        type.HasInterfaceInBases(typeText.FullName, out foundInterface);
    
    public static bool HasInterfaceInBases(this ITypeSymbol type, string name, out INamedTypeSymbol? foundInterface)
    {
        foundInterface = null;
        
        var baseType = type.BaseType;
        if (baseType is null) return false;
        
        foreach (var @interface in baseType.AllInterfaces)
        {
            if (@interface.ToDisplayString() != name) continue;
            
            foundInterface = @interface;
            return true;
        }

        return false;
    }
    
    public static bool HasInterfaceInSelfOrBases(this ITypeSymbol type, TypeText typeText) =>
        type.HasInterfaceInSelfOrBases(typeText.FullName);
    
    public static bool HasInterfaceInSelfOrBases(this ITypeSymbol type, string name) =>
        type.AllInterfaces.Any(@interface =>  @interface.ToDisplayString() == name);

    public static bool HasInterfaceInSelfOrBases(this ITypeSymbol type, TypeText typeText, out INamedTypeSymbol? foundInterface) =>
        type.HasInterfaceInSelfOrBases(typeText.FullName, out foundInterface);
    
    public static bool HasInterfaceInSelfOrBases(this ITypeSymbol type, string name, out INamedTypeSymbol? foundInterface)
    {
        foundInterface = null;
        
        foreach (var @interface in type.AllInterfaces)
        {
            if (@interface.ToDisplayString() != name) continue;
            
            foundInterface = @interface;
            return true;
        }

        return false;
    }
}