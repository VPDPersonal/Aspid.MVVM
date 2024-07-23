using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.ViewModels;

public readonly struct ViewModelData(
    bool hasViewModelBaseType,
    bool hasViewModelInterface,
    TypeDeclarationSyntax declaration,
    IReadOnlyCollection<FieldData> fields, 
    IReadOnlyCollection<IMethodSymbol> methods)
{
    public readonly bool HasViewModelBaseType = hasViewModelBaseType;
    public readonly bool HasViewModelInterface = hasViewModelInterface;
        
    public readonly TypeDeclarationSyntax Declaration = declaration;
    public readonly IReadOnlyCollection<FieldData> Fields = fields;
    public readonly IReadOnlyCollection<IMethodSymbol> Methods = methods;
}