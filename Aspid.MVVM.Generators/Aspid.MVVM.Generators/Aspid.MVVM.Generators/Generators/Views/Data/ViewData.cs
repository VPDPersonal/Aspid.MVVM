using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;

namespace Aspid.MVVM.Generators.Generators.Views.Data;

public readonly struct ViewData(
    INamedTypeSymbol symbol,
    Inheritor inheritor, 
    TypeDeclarationSyntax declaration,
    ImmutableArray<BinderMember> members,
    ImmutableArray<GenericViewData> genericViews)
{
    public readonly INamedTypeSymbol Symbol = symbol; 
    public readonly Inheritor Inheritor = inheritor;
    public readonly ImmutableArray<BinderMember> Members = members;
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly ImmutableArray<GenericViewData> GenericViews = genericViews;
}