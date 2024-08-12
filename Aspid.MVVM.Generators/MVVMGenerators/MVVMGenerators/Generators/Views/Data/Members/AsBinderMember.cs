using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct AsBinderMember(ISymbol member, ITypeSymbol asBinderType) : IMember
{
    public ITypeSymbol? Type => member switch
    {
        IFieldSymbol field => field.Type,
        IPropertySymbol property => property.Type,
                
        // TODO Add Log
        _ => null
    };
    
    public string Name => member.Name;

    public ITypeSymbol AsBinderType => asBinderType;

    public string BinderName => member is IPropertySymbol property
        ? $"{property.GetFieldName()}Binder"
        : $"{member.Name}Binder";

    public string Id => $"{FieldSymbolExtensions.GetPropertyNameFromFieldName(member.Name)}Id";
    
    public bool IsUnityEngineObject => Type switch
    {
        IArrayTypeSymbol => false,
        _ => Type?.HasBaseType(Classes.Object) ?? false
    };
}
