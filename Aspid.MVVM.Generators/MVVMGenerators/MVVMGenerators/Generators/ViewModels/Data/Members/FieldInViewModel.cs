using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public readonly struct FieldInViewModel(
    IFieldSymbol field,
    bool isReadOnly,
    SyntaxKind getAccess,
    SyntaxKind setAccess,
    IEnumerable<IPropertySymbol> bindAlso)
{
    public readonly ITypeSymbol Type = field.Type;
    
    public readonly string Name = field.Name;
    public readonly string PropertyName = field.GetPropertyName();
    
    public readonly bool IsReadOnly = isReadOnly;
    public readonly SyntaxKind SetAccess = setAccess;
    public readonly SyntaxKind GetAccess = getAccess;
    
    public readonly ImmutableArray<IPropertySymbol> BindAlso = ImmutableArray.CreateRange(bindAlso);
}