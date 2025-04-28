using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Ids;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public readonly struct BindAlsoProperty(IPropertySymbol property) : IEquatable<BindAlsoProperty>
{
    public readonly ITypeSymbol Type = property.Type;
    public readonly IPropertySymbol Property = property;
    
    public readonly string Name = property.Name;
    public readonly string EventName = $"{property.Name}Changed";
    public readonly string Id = $"{Classes.Ids.Global}.{property.GetId()}";
    public readonly string ViewModelEventName = $"__{property.GetFieldName(false)}ChangedEvent";
    
    public override bool Equals(object? obj) =>
        obj is BindAlsoProperty other && Equals(other);

    public bool Equals(BindAlsoProperty other) =>
        SymbolEqualityComparer.Default.Equals(Property, other.Property);

    public override int GetHashCode() =>
        SymbolEqualityComparer.Default.GetHashCode(Property);
}