using System;
using System.ComponentModel;

namespace Aspid.MVVM
{
    public readonly struct Id : IEquatable<Id>
    {
        public readonly int Length;
        public readonly int HashCode;
        public readonly string Value;

        public Id(string value) : this()
        {
            Value = value;
            Length = value.Length;
            HashCode = value.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Id(string value, int hasCode)
        {
            Value = value;
            HashCode = hasCode;
            Length = value.Length;
        }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Id(string value, int length, int hasCode)
        {
            Value = value;
            Length = length;
            HashCode = hasCode;
        }

        public bool Equals(Id other) =>
            HashCode == other.HashCode && Value == other.Value;
        
        public override bool Equals(object? obj) =>
            obj is Id other && Equals(other);

        public override int GetHashCode() =>
            HashCode;

        public static implicit operator Id(string value) =>
            new(value);
    }
}