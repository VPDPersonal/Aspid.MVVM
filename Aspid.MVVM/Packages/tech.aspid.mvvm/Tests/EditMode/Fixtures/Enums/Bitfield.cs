// ReSharper disable once CheckNamespace
using Aspid.MVVM.StarterKit;

namespace Aspid.MVVM.Tests
{
    // A member above int.MaxValue that only an unsigned backing type can hold.
    internal enum Bitfield : uint
    {
        Empty = 0,
        Full = uint.MaxValue,
    }
}
