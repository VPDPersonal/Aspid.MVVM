using System;
using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Generators.Descriptions;
using Aspid.MVVM.Generators.Generators.Ids.Extensions;

namespace Aspid.MVVM.Generators.Generators.Ids.Data;

public readonly struct IdData : IEquatable<IdData>
{
    public readonly int Length;
    public readonly string Value;
    public readonly string SourceValue;

    public IdData(ISymbol member, string postfix = "")
    {
        SourceValue = member.GetId(postfix);
        
        Length = SourceValue.Length;
        Value = $"{Classes.Ids}.{SourceValue}";
    }

    public override bool Equals(object? obj) =>
        obj is IdData other && Equals(other);
    
    public bool Equals(IdData other) =>
        Length == other.Length && Value == other.Value;

    public override int GetHashCode() => 
        Value.GetHashCode();
    
    public override string ToString() => Value;

    public static implicit operator string(IdData id) => id.Value;
}