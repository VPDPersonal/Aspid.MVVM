using System;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public readonly struct ViewModelEvent
{
    public readonly bool Has;
    public readonly string? Type;
    public readonly string? Name;
    public readonly string? EventType;
    public readonly string? FieldName;
    
    private readonly BindMode _mode;
    private readonly string _generatedName;

    public ViewModelEvent(BindMode mode, string generatedName, string type)
    {
        _mode = mode;
        _generatedName = generatedName;
        
        if (mode is BindMode.None) return;
        
        EventType = mode switch
        {
            BindMode.OneWay => Classes.OneWayBindableMemberEvent.Global,
            BindMode.TwoWay => Classes.TwoWayBindableMemberEvent.Global,
            BindMode.OneTime => Classes.OneTimeBindableMemberEvent.Global,
            BindMode.OneWayToSource => Classes.OneWayToSourceBindableMemberEvent.Global,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

        Type = type;
        Has = EventType is not null;
        Name = mode is BindMode.OneWay or BindMode.TwoWay ? $"{generatedName}Changed" : null;
        FieldName = $"__{FieldSymbolExtensions.RemovePrefix(PropertySymbolExtensions.GetFieldName(generatedName, false))}ChangedEvent";
    }

    public string ToInstantiateFieldString() => _mode switch
    {
        BindMode.OneWay => $"{FieldName} ??= new({_generatedName})",
        BindMode.TwoWay => $"{FieldName} ??= new({_generatedName}, Set{_generatedName})",
        BindMode.OneTime => $"{FieldName} ??= new({_generatedName})",
        BindMode.OneWayToSource => $"{FieldName} ??= new(Set{_generatedName})",
        _ => string.Empty
    };
}