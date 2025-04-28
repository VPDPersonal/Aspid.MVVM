using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Ids;
using MVVMGenerators.Helpers.Descriptions;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct BinderFieldInView(IFieldSymbol field)
{
    public readonly IFieldSymbol Field = field;
    public readonly string FieldName = field.Name;
    public readonly string Id = $"{Classes.Ids.Global}.{field.GetId()}";
}