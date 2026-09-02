using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ReverseStringConverter"/> — surrogate pairs and combining marks under
    /// reversal.
    /// </summary>
    [TestFixture]
    public sealed class ReverseStringConverterTests
    {
        [TestCase("abc", "cba")]
        [TestCase("a", "a")]
        [TestCase("ab", "ba")]
        [TestCase("", "")]
        public void Convert_WritesTheStringBackToFront(string value, string expected) =>
            Assert.AreEqual(expected, new ReverseStringConverter().Convert(value));

        // Reversing spaces would produce the same characters, so the reference is what pins the guard.
        [Test]
        public void Convert_BlankIsReturnedUnchanged()
        {
            const string value = " \t ";

            Assert.AreSame(value, new ReverseStringConverter().Convert(value));
        }

        [Test]
        public void Convert_NullIsReturnedUnchanged() =>
            Assert.IsNull(new ReverseStringConverter().Convert(null));

        // A surrogate pair is one character stored as two, and the pair keeps its internal order while
        // everything around it reverses. A plain char-by-char reversal produces two lone surrogates
        // here and renders as a replacement box.
        [Test]
        public void Convert_KeepsSurrogatePairsInOrder() =>
            Assert.AreEqual("dc😀ba", new ReverseStringConverter().Convert("ab😀cd"));

        [Test]
        public void Convert_SwapsWholePairsRatherThanCodeUnits() =>
            Assert.AreEqual(
                "🎉😀",
                new ReverseStringConverter().Convert("😀🎉"));

        [Test]
        public void Convert_OfAReversedPairStringIsTheOriginal()
        {
            const string value = "ab😀cd";
            var converter = new ReverseStringConverter();

            Assert.AreEqual(value, converter.Convert(converter.Convert(value)));
        }

        // Malformed input reaches converters from user-typed text and truncated network strings. A
        // surrogate with no partner is copied as an ordinary code unit rather than pairing with the
        // letter beside it.
        [Test]
        public void Convert_LoneHighSurrogateIsCopiedAsIs() =>
            Assert.AreEqual("b\uD83Da", new ReverseStringConverter().Convert("a\uD83Db"));

        // The pair test reads value[i - 1], so a low surrogate at index 0 is where an unguarded read
        // walks off the front of the string. The loop's "i > 0" is the only thing preventing it.
        [Test]
        public void Convert_LoneLowSurrogateAtIndexZeroDoesNotReadPastTheStart() =>
            Assert.AreEqual("ba\uDE00", new ReverseStringConverter().Convert("\uDE00ab"));

        // The guard is directional — high at i-1, low at i — so a low/high sequence is not a pair and
        // is reversed like any other two characters. Reversing malformed input can therefore hand back
        // a well-formed emoji.
        [Test]
        public void Convert_LowThenHighIsNotTreatedAsAPair() =>
            Assert.AreEqual("😀", new ReverseStringConverter().Convert("\uDE00\uD83D"));

        // A combining mark (U+0301) decorates the character before it, and plain reversal puts
        // the mark in front of its old host and behind whatever used to follow it — so here the
        // accent moves off "e" and onto "y".
        [Test]
        public void Convert_CombiningMarkMovesOntoTheFollowingCharacter() =>
            Assert.AreEqual("y\u0301ex", new ReverseStringConverter().Convert("xe\u0301y"));

        // With nothing after it the mark ends up at index 0, decorating nothing at all.
        [Test]
        public void Convert_TrailingCombiningMarkEndsUpDanglingAtTheStart() =>
            Assert.AreEqual("\u0301eba", new ReverseStringConverter().Convert("abe\u0301"));

        // Without the Clear() the second call would return "cbazyx".
        [Test]
        public void Convert_ReusingOneInstanceDoesNotAccumulate()
        {
            var converter = new ReverseStringConverter();

            Assert.AreEqual("cba", converter.Convert("abc"));
            Assert.AreEqual("zyx", converter.Convert("xyz"), "the cached builder leaked the previous call");
        }
    }
}
