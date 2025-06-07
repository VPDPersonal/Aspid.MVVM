using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Views.Data.Members;

namespace Aspid.MVVM.Generators.Views.Data;

public readonly struct ViewData(
    ISymbol symbol,
    Inheritor inheritor, 
    TypeDeclarationSyntax declaration,
    ImmutableArray<BinderMember> members,
    ImmutableArray<ITypeSymbol> genericViews)
{
    public readonly ISymbol Symbol = symbol; 
    public readonly Inheritor Inheritor = inheritor;
    public readonly ImmutableArray<BinderMember> Members = members;
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly ImmutableArray<ITypeSymbol> GenericViews = genericViews;
}