using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels.Data;

namespace MVVMGenerators.Generators.ViewModels;

public readonly struct ViewModelData(
    bool hasViewModelBaseType,
    bool hasViewModelInterface,
    TypeDeclarationSyntax declaration,
    IEnumerable<FieldData> fields,
    IEnumerable<RelayCommandData> commands)
{
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly bool HasViewModelBaseType = hasViewModelBaseType;
    public readonly bool HasViewModelInterface = hasViewModelInterface;
    
    public readonly ImmutableArray<FieldData> Fields = ImmutableArray.CreateRange(fields);
    public readonly ImmutableArray<RelayCommandData> Commands = ImmutableArray.CreateRange(commands);
}