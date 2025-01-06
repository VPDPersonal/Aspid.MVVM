using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct BinderFieldInView(IFieldSymbol field)
{
    public readonly IFieldSymbol Field = field;
    public readonly string FieldName = field.Name;
    public readonly string Id = $"{field.GetPropertyName()}Id";
}