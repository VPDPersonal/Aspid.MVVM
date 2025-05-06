using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public readonly struct ViewModelEvent(BindMode mode, ISymbol member)
{
    public readonly string? Type = member switch
    {
        IFieldSymbol field => field.Type.ToDisplayStringGlobal(),
        IPropertySymbol property => property.Type.ToDisplayStringGlobal(),
        _ => null
    };
    
    public readonly string? Name = mode is BindMode.OneTime or BindMode.OneWayToSource ?
        null
        : $"{member.GetPropertyName()}Changed";
    
    public readonly string? FieldName = mode is BindMode.OneTime
        ? null
        : $"__{FieldSymbolExtensions.RemovePrefix(member.GetFieldName(false))}ChangedEvent";
    
    public readonly string? EventType = mode switch
    {
        BindMode.OneWay => Classes.OneWayViewModelEvent.Global,
        BindMode.TwoWay => Classes.TwoWayViewModelEvent.Global,
        BindMode.OneTime => null,
        BindMode.OneWayToSource => Classes.OneWayToSourceViewModelEvent.Global,
        _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
    };
    
    public bool Has => EventType is not null;
}