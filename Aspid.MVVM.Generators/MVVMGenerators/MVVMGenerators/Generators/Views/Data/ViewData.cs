using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Data;

public readonly struct ViewData(
    Inheritor inheritor, 
    TypeDeclarationSyntax declaration,
    ImmutableArray<BinderMember> members,
    ImmutableArray<ITypeSymbol> genericViews)
{
    public readonly Inheritor Inheritor = inheritor;
    public readonly ImmutableArray<BinderMember> Members = members;
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly ImmutableArray<ITypeSymbol> GenericViews = genericViews;
}