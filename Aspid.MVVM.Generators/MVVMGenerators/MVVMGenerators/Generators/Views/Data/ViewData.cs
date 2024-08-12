using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Data;

public readonly struct ViewData(
    Inheritor inheritor, 
    TypeDeclarationSyntax declaration, 
    ImmutableArray<FieldMember> otherMembers, 
    ImmutableArray<PropertyMember> propertyMembers, 
    ImmutableArray<AsBinderMember> asBinderMembers)
{
    public readonly Inheritor Inheritor = inheritor;
    public readonly TypeDeclarationSyntax Declaration = declaration;

    public readonly ImmutableArray<FieldMember> FieldMembers = otherMembers;
    public readonly ImmutableArray<PropertyMember> PropertyMembers = propertyMembers;
    public readonly ImmutableArray<AsBinderMember> AsBinderMembers = asBinderMembers;
}