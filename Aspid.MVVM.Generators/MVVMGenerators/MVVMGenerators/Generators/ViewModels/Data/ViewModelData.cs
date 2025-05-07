using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members.Collections;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly struct ViewModelData(
    Inheritor inheritor,
    INamedTypeSymbol classSymbol,
    TypeDeclarationSyntax declaration,
    ImmutableArray<BindableMember> members,
    ImmutableArray<HasCodeMemberGroup> hashCodeMemberGroups,
    ImmutableArray<IdLengthMemberGroup> idLengthMemberGroups)
{
    public readonly Inheritor Inheritor = inheritor;
    public readonly string Name = declaration.Identifier.Text;
    public readonly INamedTypeSymbol ClassSymbol = classSymbol;
    public readonly TypeDeclarationSyntax Declaration = declaration;

    public readonly ImmutableArray<BindableMember> Members = members;
    public readonly ImmutableArray<HasCodeMemberGroup> HashCodeMemberGroups = hashCodeMemberGroups;
    public readonly ImmutableArray<IdLengthMemberGroup> IdLengthMemberGroups = idLengthMemberGroups;
}