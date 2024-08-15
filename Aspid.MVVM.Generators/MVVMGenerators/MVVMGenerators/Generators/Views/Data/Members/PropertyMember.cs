using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct PropertyMember(IPropertySymbol property)
{
    public readonly string Name = property.Name;
    public readonly string FieldName = property.GetFieldName();
    
    public readonly string Id = $"{property.Name}Id";
    public readonly ITypeSymbol Type = property.Type;
    
    public readonly bool IsUnityEngineObject = property.Type switch
    {
        IArrayTypeSymbol => false,
        _ => property.Type.HasBaseType(Classes.Object)
    };
}