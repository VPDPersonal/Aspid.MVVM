using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct PropertyMember(IPropertySymbol property, bool isView = false) : IMember
{
    public bool IsView => isView;
    
    public string Name => property.Name;

    public string FieldName => property.GetFieldName();
    
    public string Id => $"{property.Name}Id";
    
    public ITypeSymbol Type => property.Type;
    
    public bool IsUnityEngineObject => Type switch
    {
        IArrayTypeSymbol => false,
        _ => Type?.HasBaseType(Classes.Object) ?? false
    };
}