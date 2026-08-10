using System;

namespace Aspid.MVVM.StarterKit.Tests
{
    // A signed backing type whose member holds the sign bit: reading it as bits sign-extends to
    // 0xFFFF_FFFF_FFFF_FF80, so a complement leaves every bit above the enum's own width set.
    [Flags]
    internal enum Channel : sbyte
    {
        Silent = 0,
        Left = 1,
        Muted = -128,
    }
}
