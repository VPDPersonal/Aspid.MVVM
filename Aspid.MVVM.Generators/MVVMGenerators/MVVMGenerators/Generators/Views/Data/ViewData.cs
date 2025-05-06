using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Data;

public readonly struct ViewData(
    Inheritor inheritor, 
    TypeDeclarationSyntax declaration,
    ImmutableArray<BinderMember> members)
{
    public readonly Inheritor Inheritor = inheritor;
    public readonly ImmutableArray<BinderMember> Members = members;
    public readonly TypeDeclarationSyntax Declaration = declaration;
}