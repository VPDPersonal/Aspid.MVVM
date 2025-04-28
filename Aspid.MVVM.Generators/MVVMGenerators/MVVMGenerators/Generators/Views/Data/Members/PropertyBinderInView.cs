using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Ids;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct PropertyBinderInView(IPropertySymbol property)
{
    public readonly ITypeSymbol Type = property.Type;
    public readonly IPropertySymbol Property = property;
    
    public readonly string PropertyName = property.Name;
    public readonly string Id = $"{Classes.Ids.Global}.{property.GetId()}";
    public readonly string CachedName = $"_{property.GetFieldName()}CachedPropertyBinder";

    public readonly bool IsUnityEngineObjectBinder = property.Type switch
    {
        IArrayTypeSymbol => false,
        _ => property.Type.HasBaseType(Classes.Object)
    };
}
