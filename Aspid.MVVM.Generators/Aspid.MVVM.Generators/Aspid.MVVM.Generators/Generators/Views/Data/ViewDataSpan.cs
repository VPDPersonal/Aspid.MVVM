using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;
using Aspid.MVVM.Generators.Generators.Views.Data.Members.Collections;

namespace Aspid.MVVM.Generators.Generators.Views.Data;

public readonly ref struct ViewDataSpan(ViewData viewData)
{
    public readonly INamedTypeSymbol Symbol = viewData.Symbol; 
    public readonly Inheritor Inheritor = viewData.Inheritor;
    public readonly TypeDeclarationSyntax Declaration = viewData.Declaration;
    public readonly ReadOnlySpan<BinderMember> Members = viewData.Members.AsSpan();
    public readonly BinderMembersCollectionSpanByType MembersByType = new(viewData.Members);
    public readonly ReadOnlySpan<GenericViewData> GenericViews = viewData.GenericViews.AsSpan();

    public bool IsInstantiateBinders => MembersByType.AsBinders.Length + MembersByType.PropertyBinders.Length > 0;
}