using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct AsBinderMemberInView
{
    public readonly string Name;
    public readonly string? CachedName;
    public readonly string AsBinderType;
    
    public readonly string? Id;
    public readonly ITypeSymbol? Type;
    public readonly bool IsUnityEngineObject;
    
    public AsBinderMemberInView(ISymbol member, ITypeSymbol asBinderType) 
        : this(member, asBinderType.ToDisplayStringGlobal()) { }

    public AsBinderMemberInView(ISymbol member, string asBinderType)
    {
        Name = member.Name;
        AsBinderType = asBinderType;

        switch (member)
        {
            case IFieldSymbol field:
                Type = field.Type;
                Id = $"{field.GetPropertyName()}Id";
                CachedName = $"_{field.Name}CachedBinder";
                break;
            
            case IPropertySymbol property:
                Type = property.Type;
                CachedName = $"_{property.GetFieldName()}CachedPropertyBinder";
                Id = $"{FieldSymbolExtensions.GetPropertyName(Name)}IdProperty";
                break;
            
            default:
                Id = null;
                Type = null;
                CachedName = null;
                break;
        }

        if (Type != null)
        {
            IsUnityEngineObject = Type switch
            {
                IArrayTypeSymbol => false,
                _ => Type?.HasBaseType(Classes.Object) ?? false
            };
        }
    }
}
