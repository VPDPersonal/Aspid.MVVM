using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly ref struct ViewModelDataSpan(ViewModelData data)
{
    public readonly bool HasBaseType = data.HasBaseType;
    public readonly Inheritor Inheritor = data.Inheritor;
    public readonly TypeDeclarationSyntax Declaration = data.Declaration;

    public readonly ReadOnlySpan<FieldInViewModel> Fields = data.Fields.AsSpan();
    public readonly ReadOnlySpan<RelayCommandData> Commands = data.Commands.AsSpan();
}