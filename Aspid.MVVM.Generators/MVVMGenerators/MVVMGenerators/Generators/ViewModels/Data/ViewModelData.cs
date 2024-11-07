using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly struct ViewModelData(
    Inheritor inheritor,
    TypeDeclarationSyntax declaration,
    IEnumerable<FieldInViewModel> fields,
    IEnumerable<RelayCommandData> commands)
{
    public readonly Inheritor Inheritor = inheritor;
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly bool HasBaseType = inheritor is Inheritor.InheritorViewModel or Inheritor.InheritorViewModelAttribute;

    public readonly ImmutableArray<FieldInViewModel> Fields = ImmutableArray.CreateRange(fields);
    public readonly ImmutableArray<RelayCommandData> Commands = ImmutableArray.CreateRange(commands);
}