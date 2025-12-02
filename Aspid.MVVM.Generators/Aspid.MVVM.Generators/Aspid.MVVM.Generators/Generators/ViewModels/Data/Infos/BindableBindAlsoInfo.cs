using System;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Ids.Data;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

public sealed class BindableBindAlsoInfo(IPropertySymbol propertySymbol) : IBindableMemberInfo, IEquatable<BindableBindAlsoInfo>
{
    private readonly IPropertySymbol _propertySymbol = propertySymbol;
    
    public string Type { get; } = propertySymbol.Type.ToDisplayStringGlobal();

    public string Name { get; } = propertySymbol.Name;
    
    public IdData Id { get; } = new(propertySymbol);

    public BindMode Mode => BindMode.OneWay;

    public GeneratedBindableMembers Bindable { get; } = GeneratedBindableMembers.CreateForBindAlso(propertySymbol);

    public override bool Equals(object? obj) =>
        obj is BindableBindAlsoInfo other && Equals(other);

    public bool Equals(BindableBindAlsoInfo other) =>
        SymbolEqualityComparer.Default.Equals(_propertySymbol, other._propertySymbol);

    public override int GetHashCode() =>
        SymbolEqualityComparer.Default.GetHashCode(_propertySymbol);
}
