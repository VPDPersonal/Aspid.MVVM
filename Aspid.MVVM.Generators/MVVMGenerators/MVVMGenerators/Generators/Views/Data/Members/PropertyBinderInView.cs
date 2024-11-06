using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct PropertyBinderInView(IPropertySymbol property)
{
    public readonly ITypeSymbol Type = property.Type;
    
    public readonly string PropertyName = property.Name;
    public readonly string CachedName = $"{property.GetFieldName()}CachedPropertyBinder";
    public readonly string Id = $"{FieldSymbolExtensions.GetPropertyName(property.Name)}IdProperty";

    public readonly bool IsUnityEngineObjectBinder = property.Type switch
    {
        IArrayTypeSymbol => false,
        _ => property.Type.HasBaseType(Classes.Object)
    };
}
