using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data.Members;
using MVVMGenerators.Generators.Views.Data.Members.Collections;

namespace MVVMGenerators.Generators.Views.Data;

public readonly ref struct ViewDataSpan(ViewData viewData)
{
    public readonly Inheritor Inheritor = viewData.Inheritor;
    public readonly TypeDeclarationSyntax Declaration = viewData.Declaration;
    public readonly ReadOnlySpan<BinderMember> Members = viewData.Members.AsSpan();
    public readonly ReadOnlySpan<ITypeSymbol> GenericViews = viewData.GenericViews.AsSpan();
    public readonly BinderMembersCollectionSpanByType MembersByType = new(viewData.Members);
}