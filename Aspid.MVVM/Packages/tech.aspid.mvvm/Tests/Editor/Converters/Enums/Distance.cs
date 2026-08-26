// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    // Backed by a long, with a member past int.MaxValue: the case a converter reading the underlying
    // value as an int cannot answer at all.
    internal enum Distance : long
    {
        None = 0,
        Far = 5_000_000_000L,
    }
}
