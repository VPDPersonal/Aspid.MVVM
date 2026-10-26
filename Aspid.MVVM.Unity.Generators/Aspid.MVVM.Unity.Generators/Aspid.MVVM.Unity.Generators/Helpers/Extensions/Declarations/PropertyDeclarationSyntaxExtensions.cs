using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.Generator.Helpers;

public static class PropertyDeclarationSyntaxExtensions
{
    public static bool HasGetAccessor(this PropertyDeclarationSyntax property) =>
        property.HasAccessor(SyntaxKind.GetAccessorDeclaration);
    
    public static bool HasSetAccessor(this PropertyDeclarationSyntax property) =>
        property.HasAccessor(SyntaxKind.SetAccessorDeclaration);

    public static bool HasInitAccessor(this PropertyDeclarationSyntax property) =>
        property.HasAccessor(SyntaxKind.InitAccessorDeclaration);
    
    private static bool HasAccessor(this PropertyDeclarationSyntax propertyDeclaration, SyntaxKind accessorKind)
    {
        var accessorList = propertyDeclaration.AccessorList;
        return accessorList != null && accessorList.Accessors.Any(accessor => accessor.Kind() == accessorKind);
    }
}