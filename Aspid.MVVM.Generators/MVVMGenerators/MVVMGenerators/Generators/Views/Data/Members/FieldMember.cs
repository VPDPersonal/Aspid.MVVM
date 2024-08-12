using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct FieldMember(IFieldSymbol field, bool isView) : IMember
{
    public bool IsView => isView;
    
    public string Name => field.Name;
    
    public ITypeSymbol Type => field.Type;

    public string Id => $"{field.GetPropertyName()}Id";
}