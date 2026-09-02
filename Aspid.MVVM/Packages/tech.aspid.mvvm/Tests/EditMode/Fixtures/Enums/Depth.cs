// ReSharper disable once CheckNamespace
using Aspid.MVVM.StarterKit;

namespace Aspid.MVVM.Tests
{
    // A negative member declared first: Enum.GetValues sorts by unsigned magnitude, so it comes
    // back last and the positions are not the declaration order.
    public enum Depth : sbyte
    {
        Below = -1,
        Surface = 0,
        Above = 1,
    }
}
