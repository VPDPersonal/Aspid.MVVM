using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct AsBinderMember
{
    public readonly string Name;
    public readonly string BinderName;
    public readonly string AsBinderType;
    
    public readonly string Id;
    public readonly ITypeSymbol? Type;
    
    public bool IsUnityEngineObject => Type switch
    {
        IArrayTypeSymbol => false,
        _ => Type?.HasBaseType(Classes.Object) ?? false
    };
    
    public AsBinderMember(ISymbol member, ITypeSymbol asBinderType) 
        : this(member, asBinderType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)) { }

    public AsBinderMember(ISymbol member, string asBinderType)
    {
        Name = member.Name;
        AsBinderType = asBinderType;

        switch (member)
        {
            case IFieldSymbol field:
                Id = $"{field.GetPropertyName()}Id";
                Type = field.Type;
                BinderName = $"{Name}Binder";
                break;
            
            case IPropertySymbol property:
                Id = $"{Name}Id";
                Type = property.Type;
                BinderName = $"{property.GetFieldName()}Binder";
                break;
        }
    }
}
