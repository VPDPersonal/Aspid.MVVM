using System;
using Microsoft.CodeAnalysis;

namespace Aspid.MVVM.Generators.Generators.Views.Data;

public readonly struct GenericViewData(bool isSelf, ITypeSymbol type) : IEquatable<GenericViewData>
{
    public readonly bool IsSelf = isSelf;
    public readonly ITypeSymbol Type = type;

    public bool Equals(GenericViewData other) =>
        SymbolEqualityComparer.Default.Equals(Type, other.Type);

    public override bool Equals(object? obj) =>
        obj is GenericViewData other && Equals(other);

    public override int GetHashCode() =>
        SymbolEqualityComparer.Default.GetHashCode(Type);
}