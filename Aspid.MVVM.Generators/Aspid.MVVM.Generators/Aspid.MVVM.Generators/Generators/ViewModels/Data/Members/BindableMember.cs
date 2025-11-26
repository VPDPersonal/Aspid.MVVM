using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Ids.Data;
using static Aspid.Generators.Helper.Classes;
using static Aspid.MVVM.Generators.Helpers.SymbolExtensions;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;

public abstract class BindableMember<T> : BindableMember
    where T : ISymbol
{
    public readonly T Member;
    
    protected BindableMember(T member, BindMode mode, string type, string sourceName, string generatedName, string idPostfix, TypeKind typeKind = TypeKind.Class) 
        : base(member, mode, type, sourceName, generatedName, idPostfix, typeKind)
    {
        Member = member;
    }
}

public abstract class BindableMember
{
    public readonly string Type;
    public readonly string SourceName;
    public readonly string GeneratedName;
    public readonly string BindableMemberPropertyType;

    public readonly IdData Id;
    public readonly BindMode Mode;
    
    private readonly string? _bindableType;
    private readonly string _bindableFieldName;

    protected BindableMember(ISymbol member, BindMode mode, string type, string name, string idPostfix, TypeKind typeKind = TypeKind.Class)
        : this(member, mode, type, name, name, idPostfix, typeKind) { }
    
    protected BindableMember(
        ISymbol member,
        BindMode mode, 
        string type,
        string sourceName,
        string generatedName, 
        string idPostfix,
        TypeKind typeKind = TypeKind.Class)
    {
        Type = type;
        Mode = mode;
        SourceName = sourceName;
        GeneratedName = generatedName;
        Id = new IdData(member, idPostfix);
        
        // Handle generic type parameters with constraints
        if (typeKind is TypeKind.TypeParameter)
        {
            if (member.GetSymbolType() is ITypeParameterSymbol typeParameter)
            {
                typeKind = GetEffectiveTypeKind(typeParameter);
            }
        }
        
        switch (mode)
        {
            case BindMode.OneWay:
                _bindableType = typeKind switch
                {
                    TypeKind.Enum => OneWayEnumBindableMember,
                    TypeKind.Struct => OneWayStructBindableMember,
                    _ => OneWayBindableMember
                }; 
                break;
            
            case BindMode.TwoWay:
                _bindableType = typeKind switch
                {
                    TypeKind.Enum => TwoWayEnumBindableMember,
                    TypeKind.Struct => TwoWayStructBindableMember,
                    _ => TwoWayBindableMember
                };
                break;
            
            case BindMode.OneTime:
                _bindableType = typeKind switch
                {
                    TypeKind.Enum => OneTimeEnumBindableMember,
                    TypeKind.Struct => OneTimeStructBindableMember,
                    _ => OneTimeBindableMember
                };
                break;
            
            case BindMode.OneWayToSource:
                _bindableType = typeKind switch
                {
                    TypeKind.Enum => OneWayToSourceEnumBindableMember,
                    TypeKind.Struct => OneWayToSourceStructBindableMember,
                    _ => OneWayToSourceBindableMember
                };
                break;
        }
        
        BindableMemberPropertyType = Mode is BindMode.OneTime
            ? IReadOnlyValueBindableMember
            : IReadOnlyBindableMember;
        
        _bindableFieldName = $"__{RemoveFieldPrefix(GetFieldName(generatedName, null))}Bindable";
    }

    public string? ToBindableMemberFieldDeclarationString()
    {
        if (Mode is BindMode.OneTime) return null;
        
        return _bindableType is null 
            ? null
            : $"""
              [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
              {GeneratedCodeViewModelAttribute}
              private {_bindableType}<{Type}> {_bindableFieldName};
              """;
    }

    public string ToBindableMemberPropertyDeclarationString()
    {
        var instantiate = Mode switch
        {
            BindMode.OneWay => $"{_bindableFieldName} ??= new({GeneratedName})",
            BindMode.TwoWay => $"{_bindableFieldName} ??= new({GeneratedName}, Set{GeneratedName})",
            BindMode.OneTime => $"{_bindableType}<{Type}>.Get({GeneratedName})",
            BindMode.OneWayToSource => $"{_bindableFieldName} ??= new(Set{GeneratedName})",
            _ => string.Empty
        };
        
        return $"""
                {GeneratedCodeViewModelAttribute}
                public {BindableMemberPropertyType}<{Type}> {GeneratedName}Bindable => 
                    {instantiate};
                """;
    }
    
    // TODO Nullable?
    public string ToInvokeBindableMemberString() => Mode is not (BindMode.OneWayToSource or BindMode.OneTime)
        ? $"this.{_bindableFieldName}?.Invoke({SourceName});" 
        : string.Empty;
    
    private static TypeKind GetEffectiveTypeKind(ITypeParameterSymbol typeParameter)
    {
        foreach (var constraint in typeParameter.ConstraintTypes)
        {
            if (constraint.TypeKind == TypeKind.Enum 
                || constraint.SpecialType == SpecialType.System_Enum)
            {
                return TypeKind.Enum;
            }
        }
        
        return typeParameter.HasValueTypeConstraint ? TypeKind.Struct : TypeKind.Class;
    }
}