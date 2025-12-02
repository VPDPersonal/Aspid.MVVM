using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using static Aspid.Generators.Helper.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

public readonly struct GeneratedBindableMembers
{
    // TODO Aspid.MVVM.Generators – Write summary
    public readonly string Declaration;
    public readonly string? Invoke;
    
    public readonly string PropertyType;

    private GeneratedBindableMembers(
        string? invoke, 
        string propertyType,
        string declaration)
    {
        Invoke = invoke;
        Declaration = declaration;
        PropertyType = propertyType;
    }

    public static GeneratedBindableMembers CreateForRelayCommand(
        string type, 
        string fieldName,
        string propertyName)
    {
        const BindMode mode = BindMode.OneTime;
        
        var fieldType = $"{OneTimeBindableMember}<{type}>";
        var propertyType = $"{IReadOnlyValueBindableMember}<{type}>";

        var declaration = RecognizeDeclaration(mode, fieldName, null, fieldType, $"{propertyName}Bindable", propertyType);
        
        return new GeneratedBindableMembers(null, propertyType, declaration);
    }
    
    public static GeneratedBindableMembers CreateForField(IFieldSymbol fieldSymbol)
    {
        var mode = fieldSymbol.GetBindMode();
        var memberName = fieldSymbol.Name;
        
        // __fieldName;
        var fieldName = $"__{fieldSymbol.RemoveFieldPrefix()}Bindable";
        var fieldType = RecognizeFieldType(fieldSymbol.Type, mode);
        
        // _fieldName -> FieldNameBindable
        var propertyName = $"{fieldSymbol.GetPropertyName()}Bindable";
        var propertyType = RecognizePropertyType(fieldSymbol.Type, mode);

        var invoke = RecognizeInvoke(mode, memberName, fieldName);
        var declaration = RecognizeDeclaration(mode, memberName, fieldName, fieldType, propertyName, propertyType);
        
        return new GeneratedBindableMembers(invoke, propertyType, declaration);
    }
    
    public static GeneratedBindableMembers CreateForBindAlso(IPropertySymbol propertySymbol)
    {
        const BindMode mode = BindMode.OneWay;
        var memberName = propertySymbol.Name;
        
        // PropertyName -> __propertyNameBindable
        var fieldName = $"{propertySymbol.GetFieldName(prefix: "__")}Bindable";
        var fieldType = RecognizeFieldType(propertySymbol.Type, mode);
        
        // PropertyName -> PropertyNameBindable
        var propertyName = $"{propertySymbol.Name}Bindable";
        var propertyType = RecognizePropertyType(propertySymbol.Type, mode);

        var invoke = RecognizeInvoke(mode, memberName, fieldName);
        var declaration = RecognizeDeclaration(mode, memberName, fieldName, fieldType, propertyName, propertyType);
        
        return new GeneratedBindableMembers(invoke, propertyType, declaration);
    }

    private static string RecognizeFieldType(ITypeSymbol type, BindMode mode)
    {
        var typeKind = type.TypeKind;
        
        if (typeKind is TypeKind.TypeParameter)
        {
            if (type is ITypeParameterSymbol typeParameter)
            {
                typeKind = GetEffectiveTypeKind(typeParameter);
            }
        }

        string result = mode switch
        {
            BindMode.OneWay => typeKind switch
            {
                TypeKind.Enum => OneWayEnumBindableMember,
                TypeKind.Struct => OneWayStructBindableMember,
                _ => OneWayBindableMember
            },
            BindMode.TwoWay => typeKind switch
            {
                TypeKind.Enum => TwoWayEnumBindableMember,
                TypeKind.Struct => TwoWayStructBindableMember,
                _ => TwoWayBindableMember
            },
            BindMode.OneTime => typeKind switch
            {
                TypeKind.Enum => OneTimeEnumBindableMember,
                TypeKind.Struct => OneTimeStructBindableMember,
                _ => OneTimeBindableMember
            },
            BindMode.OneWayToSource => typeKind switch
            {
                TypeKind.Enum => OneWayToSourceEnumBindableMember,
                TypeKind.Struct => OneWayToSourceStructBindableMember,
                _ => OneWayToSourceBindableMember
            },
            _ => string.Empty
        };
        
        return string.IsNullOrWhiteSpace(result)
            ? string.Empty
            : $"{result}<{type.ToDisplayStringGlobal()}>";

        TypeKind GetEffectiveTypeKind(ITypeParameterSymbol typeParameter)
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

    private static string RecognizePropertyType(ITypeSymbol type, BindMode mode)
    {
        string result = mode switch
        {
            BindMode.TwoWay => IBindableMember,
            BindMode.OneWay => IReadOnlyBindableMember,
            BindMode.OneTime or BindMode.OneWayToSource => IReadOnlyValueBindableMember,
            _ => string.Empty
        };

        return string.IsNullOrWhiteSpace(result) 
            ? string.Empty 
            : $"{result}<{type.ToDisplayStringGlobal()}>";
    }

    private static string RecognizeDeclaration(
        BindMode mode,
        string memberName,
        string? fieldName, 
        string fieldType,
        string propertyName,
        string propertyType)
    {
        if (mode is BindMode.OneTime)
        {
            return
                $"""
                {GeneratedCodeViewModelAttribute}
                public {propertyType} {propertyName} => 
                    {fieldType}.Get({memberName});
                """;
        }

        var instantiate = mode switch
        {
            BindMode.OneWay => $"{fieldName} ??= new({memberName})",
            BindMode.TwoWay => $"{fieldName} ??= new({memberName}, Set{memberName})",
            BindMode.OneWayToSource => $"{fieldName} ??= new(Set{memberName})",
            _ => string.Empty
        };
        
        return
            $"""
            [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
            {GeneratedCodeViewModelAttribute}
            private {fieldType} {fieldName};
            
            {GeneratedCodeViewModelAttribute}
            public {propertyType} {propertyName} => 
                {instantiate};
            """;
    }

    private static string? RecognizeInvoke(BindMode mode, string memberName, string fieldName)
    {
        return mode is not (BindMode.OneWayToSource or BindMode.OneTime) 
            ? $"this.{fieldName}?.Invoke({memberName});" 
            : null;
    }
}
