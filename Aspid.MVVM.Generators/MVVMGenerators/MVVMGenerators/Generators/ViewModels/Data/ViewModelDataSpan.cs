using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels.Data;

namespace MVVMGenerators.Generators.ViewModels;

public readonly ref struct ViewModelDataSpan(ViewModelData data)
{
    public readonly TypeDeclarationSyntax Declaration = data.Declaration;
    public readonly bool HasViewModelBaseType = data.HasViewModelBaseType;
    public readonly bool HasViewModelInterface = data.HasViewModelInterface;

    public readonly ReadOnlySpan<FieldData> Fields = data.Fields.AsSpan();
    public readonly ReadOnlySpan<RelayCommandData> Commands = data.Commands.AsSpan();
}