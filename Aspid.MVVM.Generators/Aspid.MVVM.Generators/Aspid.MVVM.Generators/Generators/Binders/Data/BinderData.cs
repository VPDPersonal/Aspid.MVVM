using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Generators.Binders.Data;

public readonly struct BinderData(
    INamedTypeSymbol symbol,
    TypeDeclarationSyntax declaration, 
    bool hasBinderLogInBaseType,
    IEnumerable<IMethodSymbol> methods)
{
    public readonly INamedTypeSymbol Symbol = symbol;
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly bool HasBinderLogInBaseType = hasBinderLogInBaseType;
    public readonly ImmutableArray<IMethodSymbol> Methods = ImmutableArray.CreateRange(methods);
}