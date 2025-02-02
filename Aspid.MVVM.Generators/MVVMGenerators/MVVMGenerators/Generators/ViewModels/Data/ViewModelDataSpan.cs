using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members.Fields;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly ref struct ViewModelDataSpan(ViewModelData data)
{
    public readonly bool HasBaseType = data.HasBaseType;
    public readonly Inheritor Inheritor = data.Inheritor;
    public readonly TypeDeclarationSyntax Declaration = data.Declaration;

    public readonly ViewModelFieldsSpan Fields = new(data.Fields);
    public readonly ReadOnlySpan<RelayCommandData> Commands = data.Commands.AsSpan();
    public readonly ReadOnlySpan<BindAlsoProperty> BindAlsoProperties = data.BindAlsoProperties.AsSpan();
}