using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly struct FieldData(IFieldSymbol field, int getAccess, int setAccess)
{
    public readonly ITypeSymbol Type = field.Type;
    
    public readonly string Name = field.Name;
    public readonly string PropertyName = field.GetPropertyName();
    
    public readonly int SetAccess = setAccess;
    public readonly int GetAccess = getAccess;
}