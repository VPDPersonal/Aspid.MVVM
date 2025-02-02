using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members.Fields;

public readonly struct ViewModelEvent(
    BindMode mode,
    IFieldSymbol field)
{
    public readonly string? Name = mode is BindMode.OneTime ?
        null
        : $"{field.GetPropertyName()}Changed";
    
    public readonly string? FieldName = mode is BindMode.None or BindMode.OneTime
        ? null
        : $"__{field.RemovePrefix()}ChangedEvent";
    
    public readonly string? EventType = mode switch
    {
        BindMode.None => null,
        BindMode.OneWay => Classes.OneWayViewModelEvent.Global,
        BindMode.TwoWay => Classes.TwoWayViewModelEvent.Global,
        BindMode.OneTime => null,
        BindMode.OneWayToSource => Classes.OneWayToSourceViewModelEvent.Global,
        _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
    };
    
    public bool Has => EventType is not null;
}