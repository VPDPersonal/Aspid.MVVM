using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members.Collections;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly ref struct ViewModelDataSpan(ViewModelData data)
{
    public readonly string Name = data.Name;
    public readonly Inheritor Inheritor = data.Inheritor;
    public readonly INamedTypeSymbol ClassSymbol = data.ClassSymbol;
    public readonly TypeDeclarationSyntax Declaration = data.Declaration;

    public readonly ReadOnlySpan<BindableMember> Members = data.Members.AsSpan(); 
    public readonly BindableMembersCollectionSpanByType MembersByType = new(data.Members);
    public readonly ReadOnlySpan<IdLengthMemberGroup> IdLengthMemberGroups = data.IdLengthMemberGroups.AsSpan();
}