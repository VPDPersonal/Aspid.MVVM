using System;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    // A plain [Flags] enum for the text parser: a combination of two members is a legal value that is
    // not a member of its own, which is what Enum.IsDefined used to throw away.
    [Flags]
    internal enum Palette
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 4,
    }
}
