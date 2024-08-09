using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.Views.Data;

public readonly ref struct ReadOnlyViewData(ViewData viewData)
{
    public readonly Inheritor Inheritor = viewData.Inheritor;
    public readonly TypeDeclarationSyntax Declaration = viewData.Declaration;

    public readonly ReadOnlySpan<IFieldSymbol> ViewFields = viewData.ViewFields.AsSpan();
    public readonly ReadOnlySpan<IFieldSymbol> BinderFields = viewData.BinderFields.AsSpan();
    public readonly ReadOnlySpan<AsBinderMember<IFieldSymbol>> AsBinderFields = viewData.AsBinderFields.AsSpan();

    public readonly ReadOnlySpan<IPropertySymbol> ViewProperties = viewData.ViewProperties.AsSpan();
    public readonly ReadOnlySpan<IPropertySymbol> BinderProperty = viewData.BinderProperties.AsSpan();
    public readonly ReadOnlySpan<AsBinderMember<IPropertySymbol>> AsBinderProperty = viewData.AsBinderProperties.AsSpan();
}