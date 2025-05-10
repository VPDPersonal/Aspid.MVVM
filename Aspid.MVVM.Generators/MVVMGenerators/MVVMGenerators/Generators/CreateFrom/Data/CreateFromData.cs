using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.CreateFrom.Data;

public readonly struct CreateFromData(
    ConstructorDeclarationSyntax declaration, 
    IMethodSymbol constructor,
    ITypeSymbol fromType)
{
    public readonly ITypeSymbol FromType = fromType;
    public readonly IMethodSymbol Constructor = constructor;
    public readonly ConstructorDeclarationSyntax Declaration = declaration;
}