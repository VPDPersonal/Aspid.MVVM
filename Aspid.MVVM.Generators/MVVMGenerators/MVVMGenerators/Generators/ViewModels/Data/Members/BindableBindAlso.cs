using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public sealed class BindableBindAlso(ISymbol member)
    : BindableMember(
        member, 
        BindMode.OneWay,
        member.GetSymbolType()?.ToDisplayStringGlobal() ?? string.Empty, 
        member.Name,
        member.Name), IEquatable<BindableBindAlso>
{
    public override bool Equals(object? obj) =>
        obj is BindableBindAlso other && Equals(other);

    public bool Equals(BindableBindAlso other) =>
        SymbolEqualityComparer.Default.Equals(Member, other.Member);

    public override int GetHashCode() =>
        SymbolEqualityComparer.Default.GetHashCode(Member);
}