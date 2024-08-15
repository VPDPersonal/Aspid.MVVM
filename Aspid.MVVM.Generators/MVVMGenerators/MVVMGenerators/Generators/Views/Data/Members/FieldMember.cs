using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct FieldMember(IFieldSymbol field) 
{
    public readonly string Name = field.Name;
    public readonly string Id = $"{field.GetPropertyName()}Id";
}