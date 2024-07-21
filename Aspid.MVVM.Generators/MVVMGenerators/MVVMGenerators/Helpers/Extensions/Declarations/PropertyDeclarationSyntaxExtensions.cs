using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Helpers.Extensions.Declarations;

public static class PropertyDeclarationSyntaxExtensions
{
    public static bool HasGetAccessor(this PropertyDeclarationSyntax propertyDeclaration)
    {
        var accessorList = propertyDeclaration.AccessorList;
        return accessorList != null && accessorList.Accessors.Any(accessor => accessor.Kind() == SyntaxKind.GetAccessorDeclaration);
    } 
    
    public static bool HasSetAccessor(this PropertyDeclarationSyntax propertyDeclaration)
    {
        var accessorList = propertyDeclaration.AccessorList;
        return accessorList != null && accessorList.Accessors.Any(accessor => accessor.Kind() == SyntaxKind.SetAccessorDeclaration);
    } 
}