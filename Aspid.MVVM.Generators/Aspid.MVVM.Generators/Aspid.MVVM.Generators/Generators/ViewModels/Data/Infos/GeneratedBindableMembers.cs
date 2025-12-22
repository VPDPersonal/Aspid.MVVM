using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using static Aspid.Generators.Helper.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using SymbolExtensions = Aspid.MVVM.Generators.Helpers.SymbolExtensions;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

public readonly struct GeneratedBindableMembers
{
    // TODO Aspid.MVVM.Generators – Write summary
    public readonly string? Invoke;
    public readonly string Declaration;
    public readonly string PropertyName;
    public readonly string PropertyType;
    public readonly string? OnPropertyChangedName;

    private GeneratedBindableMembers(
        string? invoke, 
        string declaration,
        string propertyName,
        string propertyType,
        string? onPropertyChangedName)
    {
        Invoke = invoke;
        Declaration = declaration;
        PropertyName = propertyName;
        PropertyType = propertyType;
        OnPropertyChangedName = onPropertyChangedName;
    }

    public static GeneratedBindableMembers CreateForRelayCommand(
        string type, 
        string memberName)
    {
        const BindMode mode = BindMode.OneTime;
        
        var fieldType = $"{OneTimeBindableMember}<{type}>";
        var propertyType = $"{IReadOnlyValueBindableMember}<{type}>";
        var propertyName = $"{memberName}Bindable";

        var declaration = RecognizeDeclaration(mode, memberName, null, fieldType, propertyName, propertyType);
        
        return new GeneratedBindableMembers(null, declaration, propertyName, propertyType, null);
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
        var declaration = RecognizeDeclaration(mode, memberName, fieldName, fieldType, propertyName, propertyType, $"Set{SymbolExtensions.GetPropertyName(memberName)}");

        var onPropertyChangedName = mode is not BindMode.OneTime and not BindMode.OneWayToSource and not BindMode.None
            ? $"On{fieldSymbol.GetPropertyName()}PropertyChanged"
            : null;
        
        return new GeneratedBindableMembers(invoke, declaration, propertyName, propertyType, onPropertyChangedName);
    }
    
    public static GeneratedBindableMembers CreateForProperty(IPropertySymbol propertySymbol)
    {
        var mode = propertySymbol.GetBindMode();
        var memberName = propertySymbol.Name;
        
        // PropertyName -> __propertyNameBindable
        var fieldName = $"{propertySymbol.GetFieldName(prefix: "__")}Bindable";
        var fieldType = RecognizeFieldType(propertySymbol.Type, mode);
        
        // PropertyName -> PropertyNameBindable
        var propertyName = $"{propertySymbol.Name}Bindable";
        var propertyType = RecognizePropertyType(propertySymbol.Type, mode);

        var invoke = RecognizeInvoke(mode, memberName, fieldName);
        var declaration = RecognizeDeclaration(mode, memberName, fieldName, fieldType, propertyName, propertyType);
        
        var onPropertyChangedName = mode is not BindMode.OneTime and not BindMode.OneWayToSource and not BindMode.None
            ? $"On{propertySymbol.Name}PropertyChanged"
            : null;
        
        return new GeneratedBindableMembers(invoke, declaration, propertyName, propertyType, onPropertyChangedName);
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
        
        return new GeneratedBindableMembers(invoke, declaration, propertyName, propertyType, $"On{propertySymbol.Name}PropertyChanged");
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

    private static string? RecognizeInvoke(BindMode mode, string memberName, string fieldName)
    {
        return mode is not (BindMode.OneWayToSource or BindMode.OneTime) 
            ? $"this.{fieldName}?.Invoke({memberName});" 
            : null;
    }
    
    private static string RecognizeDeclaration(
        BindMode mode,
        string memberName,
        string? fieldName, 
        string fieldType,
        string propertyName,
        string propertyType,
        string? setMethod = null)
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

        setMethod ??= $"value => {memberName} = value";

        // For properties, we use a property setter directly instead of a method reference
        var instantiate = mode switch
        {
            BindMode.OneWay => $"{fieldName} ??= new({memberName})",
            BindMode.TwoWay => $"{fieldName} ??= new({memberName}, {setMethod})",
            BindMode.OneWayToSource => $"{fieldName} ??= new({setMethod})",
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
}
