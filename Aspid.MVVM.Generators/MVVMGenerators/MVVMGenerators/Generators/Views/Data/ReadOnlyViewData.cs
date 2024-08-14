using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Data;

public readonly ref struct ViewDataSpan(ViewData viewData)
{
    public readonly Inheritor Inheritor = viewData.Inheritor;
    public readonly TypeDeclarationSyntax Declaration = viewData.Declaration;
    
    public readonly ReadOnlySpan<FieldMember> FieldMembers = viewData.FieldMembers.AsSpan();
    public readonly ReadOnlySpan<PropertyMember> PropertyMembers = viewData.PropertyMembers.AsSpan();
    public readonly ReadOnlySpan<AsBinderMember> AsBinderMembers = viewData.AsBinderMembers.AsSpan();
}