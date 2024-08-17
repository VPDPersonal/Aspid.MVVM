using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.Binders.Data;

public readonly struct BinderData(
    TypeDeclarationSyntax declaration, 
    bool hasBinderLogInBaseType,
    IEnumerable<IMethodSymbol> methods)
{
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly bool HasBinderLogInBaseType = hasBinderLogInBaseType;
    public readonly ImmutableArray<IMethodSymbol> Methods = ImmutableArray.CreateRange(methods);
}