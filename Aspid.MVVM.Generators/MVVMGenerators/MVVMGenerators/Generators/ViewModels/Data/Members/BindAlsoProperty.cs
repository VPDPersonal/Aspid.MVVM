using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public readonly struct BindAlsoProperty(IPropertySymbol property) : IEquatable<BindAlsoProperty>
{
    public readonly ITypeSymbol Type = property.Type;
    
    public readonly string Name = property.Name;
    public readonly string Id = $"{property.Name}Id";
    public readonly string EventName = $"{property.Name}Changed";
    public readonly string ViewModelEventName = $"__{property.GetFieldName(false)}ChangedEvent";

    private readonly IPropertySymbol _property = property;

    public override bool Equals(object? obj) =>
        obj is BindAlsoProperty other && Equals(other);

    public bool Equals(BindAlsoProperty other) =>
        SymbolEqualityComparer.Default.Equals(_property, other._property);

    public override int GetHashCode() =>
        SymbolEqualityComparer.Default.GetHashCode(_property);
}