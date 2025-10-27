using System;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;

public sealed class BindableBindAlso : BindableMember<ISymbol>, IEquatable<BindableBindAlso>
{
    public BindableBindAlso(ISymbol member) 
        : base(member, BindMode.OneWay, member.GetSymbolType()?.ToDisplayStringGlobal() ?? string.Empty, member.Name, member.Name, string.Empty, member.GetSymbolType()?.TypeKind ?? TypeKind.Class) { }

    public override bool Equals(object? obj) =>
        obj is BindableBindAlso other && Equals(other);

    public bool Equals(BindableBindAlso other) =>
        SymbolEqualityComparer.Default.Equals(Member, other.Member);

    public override int GetHashCode() =>
        SymbolEqualityComparer.Default.GetHashCode(Member);
}