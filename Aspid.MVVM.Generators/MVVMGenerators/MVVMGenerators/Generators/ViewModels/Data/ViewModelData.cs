using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members.Fields;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly struct ViewModelData(
    Inheritor inheritor,
    TypeDeclarationSyntax declaration,
    ViewModelFields fields,
    IEnumerable<RelayCommandData> commands,
    IEnumerable<BindAlsoProperty> bindAlsoProperties)
{
    public readonly Inheritor Inheritor = inheritor;
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly bool HasBaseType = inheritor is Inheritor.InheritorViewModelAttribute;

    public readonly ViewModelFields Fields = fields;
    public readonly ImmutableArray<RelayCommandData> Commands = ImmutableArray.CreateRange(commands);
    public readonly ImmutableArray<BindAlsoProperty> BindAlsoProperties = ImmutableArray.CreateRange(bindAlsoProperties);
}