using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Ids.Extensions;

namespace MVVMGenerators.Generators.Ids.Data;

public readonly struct IdData : IEquatable<IdData>
{
    public readonly int Length;
    public readonly int HashCode;
    public readonly string Value;
    public readonly string SourceValue;

    public IdData(ISymbol member, string postfix = "")
    {
        SourceValue = member.GetId(postfix);
        
        Length = SourceValue.Length;
        HashCode = SourceValue.GetHashCode();
        Value = $"{Classes.Ids}.{SourceValue}";
    }

    public override bool Equals(object? obj) =>
        obj is IdData other && Equals(other);
    
    public bool Equals(IdData other) =>
        Length == other.Length && HashCode == other.HashCode && Value == other.Value;

    public override int GetHashCode() => 
        HashCode;
    
    public override string ToString() => Value;

    public string ToInstanceString() => $"new({Value}, {Length}, {HashCode})";

    public static implicit operator string(IdData id) => id.Value;
}