using System;

namespace Aspid.MVVM.StarterKit.Tests
{
    // Powers of two with no combined member declared, so a combination is a legal value the member
    // list does not hold, and position 3 (Shock) differs from value 3 (Fire | Ice).
    [Flags]
    internal enum Hazard
    {
        None = 0,
        Fire = 1,
        Ice = 2,
        Shock = 4,
    }
}
