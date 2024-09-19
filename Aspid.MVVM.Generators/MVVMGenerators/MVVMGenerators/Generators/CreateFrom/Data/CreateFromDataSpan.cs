using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.CreateFrom.Data;

public readonly ref struct CreateFromDataSpan(CreateFromData data)
{
    public readonly ITypeSymbol FromType = data.FromType;
    public readonly TypeDeclarationSyntax Declaration = data.Declaration;
    public readonly ReadOnlySpan<IMethodSymbol> Constructors = data.Constructors.AsSpan();
}