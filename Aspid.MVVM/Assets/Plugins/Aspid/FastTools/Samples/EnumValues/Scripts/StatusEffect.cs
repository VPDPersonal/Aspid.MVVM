using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.EnumValues
{
    [Flags]
    public enum StatusEffect
    {
        None = 0,
        Burning = 1,
        Frozen = 2,
        Slowed = 4,
        Stunned = 8,
        // Combinations such as Burning | Slowed are matched via HasFlag semantics in EnumValues
        // and can be registered as their own entry with a dedicated value.
    }
}
