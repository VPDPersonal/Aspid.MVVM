// ReSharper disable once CheckNamespace
using Aspid.MVVM.StarterKit;

namespace Aspid.MVVM.Tests
{
    // Sparse values, so a member's position and its underlying number disagree from the second
    // member on — the case a dropdown index hits and a straight cast gets wrong.
    public enum Medal
    {
        None = 0,
        Bronze = 10,
        Silver = 20,
    }
}
