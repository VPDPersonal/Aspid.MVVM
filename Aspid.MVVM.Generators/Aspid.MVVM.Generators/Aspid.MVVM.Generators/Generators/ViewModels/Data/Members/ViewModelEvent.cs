using Microsoft.CodeAnalysis;
using static Aspid.Generator.Helpers.SymbolExtensions;
using static Aspid.MVVM.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Descriptions.General;

namespace Aspid.MVVM.Generators.ViewModels.Data.Members;

public readonly struct ViewModelEvent
{
    public readonly bool IsExist;
    public readonly bool IsEventExist;
    
    public readonly string? Type;
    public readonly string? ValueType;
    
    public readonly string? FieldName;
    public readonly string? EventName;
    
    private readonly BindMode _mode;
    private readonly string _sourceName;
    private readonly string _generatedName;

    public ViewModelEvent(BindMode mode, string sourceName, string generatedName, string valueType, TypeKind typeKind)
    {
        _mode = mode;
        _sourceName = sourceName;
        _generatedName = generatedName;
        if (mode is BindMode.None) return;

        switch (mode)
        {
            case BindMode.OneWay:
                Type = typeKind switch
                {
                    TypeKind.Enum => OneWayEnumEvent,
                    TypeKind.Struct => OneWayStructEvent,
                    _ => OneWayClassEvent
                }; 
                break;
            
            case BindMode.TwoWay:
                Type = typeKind switch
                {
                    TypeKind.Enum => TwoWayEnumEvent,
                    TypeKind.Struct => TwoWayStructEvent,
                    _ => TwoWayClassEvent
                };
                break;
            
            case BindMode.OneTime:
                Type = typeKind switch
                {
                    TypeKind.Enum => OneTimeEnumEvent,
                    TypeKind.Struct => OneTimeStructEvent,
                    _ => OneTimeClassEvent
                };
                break;
            
            case BindMode.OneWayToSource:
                Type = typeKind switch
                {
                    TypeKind.Enum => OneWayToSourceEnumEvent,
                    TypeKind.Struct => OneWayToSourceStructEvent,
                    _ => OneWayToSourceClassEvent
                };
                break;
        }
        
        if (Type is null) return;
        
        IsExist = true;
        ValueType = valueType;
        FieldName = $"__{RemoveFieldPrefix(GetFieldName(generatedName, null))}ChangedEvent";

        EventName = mode is BindMode.OneWay or BindMode.TwoWay
            ? $"{generatedName}Changed"
            : null;
        
        IsEventExist = EventName is not null;
    }

    // TODO Nullable?
    public string ToInvokeString() => IsExist && _mode is not (BindMode.OneWayToSource or BindMode.OneTime)
        ? $"{FieldName}?.Invoke({_sourceName});"
        : string.Empty;

    // TODO Nullable?
    public string ToEventDeclarationString()
    {
        return IsEventExist
            ? $$"""
                {{GeneratedCodeViewModelAttribute}}
                public event {{Action}}<{{ValueType}}> {{EventName}}
                {
                    add
                    {
                        {{ToInstantiateFieldString()}};
                        {{FieldName}}.Changed += value;
                    }
                    remove
                    {
                        if ({{FieldName}} is null) return;
                        {{FieldName}}.Changed -= value;
                    }
                }
                """
            : string.Empty;
    }

    // TODO Nullable?
    public string ToFieldDeclarationString()
    {
        return IsExist
            ? $"""
               [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
               {GeneratedCodeViewModelAttribute}
               private {Type}<{ValueType}> {FieldName};
               """
            : string.Empty;
    }

    // TODO Nullable?
    public string ToInstantiateFieldString() => IsExist ? _mode switch
    {
        BindMode.OneWay => $"{FieldName} ??= new({_generatedName})",
        BindMode.TwoWay => $"{FieldName} ??= new({_generatedName}, Set{_generatedName})",
        BindMode.OneTime => $"{FieldName} ??= new({_generatedName})",
        BindMode.OneWayToSource => $"{FieldName} ??= new(Set{_generatedName})",
        _ => string.Empty
    } : string.Empty;
}