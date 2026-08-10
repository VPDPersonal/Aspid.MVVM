using System;

namespace Aspid.MVVM.StarterKit.Tests
{
    // Hazard ([Flags] None/Fire/Ice/Shock), Medal (sparse, not [Flags]) and Quest (label attributes)
    // are declared in EnumConverterAdditionsTests.cs and reused here.

    // A composite declared FIRST on purpose. Enum.GetValues sorts by unsigned underlying value, and a
    // composite ORs its parts so it can never sort ahead of them — the value is written as a literal
    // rather than as Fire | Ice | Poison to keep the declaration order visible.
    [Flags]
    internal enum Element
    {
        All = 7,
        None = 0,
        Fire = 1,
        Ice = 2,
        Poison = 4,
    }
}
