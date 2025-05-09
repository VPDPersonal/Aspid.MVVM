using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public sealed class BindableBindAlso : BindableMember, IBindableViewModelEvent, IEquatable<BindableBindAlso>
{
    public override string Type { get; }
    
    public ViewModelEvent Event { get; }
    
    public BindableBindAlso(ISymbol member)
        : base(member, BindMode.OneWay, member.Name, member.Name)
    {
        Event = new ViewModelEvent(BindMode.OneWay, member);
        Type = member.GetSymbolType()?.ToDisplayStringGlobal() ?? string.Empty;
    }
    
    public override bool Equals(object? obj) =>
        obj is BindableBindAlso other && Equals(other);

    public bool Equals(BindableBindAlso other) =>
        SymbolEqualityComparer.Default.Equals(Member, other.Member);

    public override int GetHashCode() =>
        SymbolEqualityComparer.Default.GetHashCode(Member);
}