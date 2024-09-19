using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.CreateFrom.Data;

public readonly struct CreateFromData(TypeDeclarationSyntax declaration, ITypeSymbol fromType, ImmutableArray<IMethodSymbol> constructors)
{
    public readonly ITypeSymbol FromType = fromType;
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly ImmutableArray<IMethodSymbol> Constructors = constructors;
}