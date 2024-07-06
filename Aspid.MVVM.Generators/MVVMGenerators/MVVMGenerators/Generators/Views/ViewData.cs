using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.Views;

public readonly struct ViewData(
    ViewInheritor inheritor,
    TypeDeclarationSyntax declaration,
    IReadOnlyCollection<IFieldSymbol> fields)
{
    public readonly ViewInheritor Inheritor = inheritor;
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly IReadOnlyCollection<IFieldSymbol> Fields = fields;
}