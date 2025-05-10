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

    public ViewModelEvent(BindMode mode, string generatedName, string type)
    {
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
}