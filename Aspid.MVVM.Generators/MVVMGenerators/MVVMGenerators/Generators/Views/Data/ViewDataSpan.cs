using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Data;

public readonly ref struct ViewDataSpan(ViewData viewData)
{
    public readonly Inheritor Inheritor = viewData.Inheritor;
    public readonly TypeDeclarationSyntax Declaration = viewData.Declaration;
    
    public readonly ReadOnlySpan<BinderFieldInView> FieldMembers = viewData.Members.Fields.AsSpan();
    public readonly ReadOnlySpan<PropertyBinderInView> ViewProperties = viewData.Members.Properties.AsSpan();
    public readonly ReadOnlySpan<AsBinderMemberInView> AsBinderMembers = viewData.Members.AsBinderMembers.AsSpan();
}