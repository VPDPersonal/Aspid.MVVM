// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    // The enum from the review that found the wrapping bug: 456 & 0xFF == 200, so any code that
    // masks an incoming integer down to the underlying width answers Legend for a number that
    // names nothing.
    internal enum ByteRank : byte
    {
        Unranked = 0,
        Legend = 200,
    }
}
