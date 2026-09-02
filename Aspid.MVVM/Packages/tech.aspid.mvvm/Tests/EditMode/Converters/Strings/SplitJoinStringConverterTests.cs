using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="SplitJoinStringConverter"/> — the part cap, part trimming and the
    /// degenerate-input guards.
    /// </summary>
    [TestFixture]
    public sealed class SplitJoinStringConverterTests
    {
        [Test]
        public void Convert_RejoinsWithTheReplacementSeparator() =>
            Assert.AreEqual(
                "sword | shield | potion",
                new SplitJoinStringConverter(",", " | ").Convert("sword,shield,potion"));

        // The parameterless constructor is what a freshly added component in the Inspector holds.
        [Test]
        public void Convert_DefaultConstructedRespacesACommaList() =>
            Assert.AreEqual("sword, shield, potion", new SplitJoinStringConverter().Convert("sword,shield,potion"));

        [Test]
        public void Convert_SplitsOnAMultiCharacterSeparator() =>
            Assert.AreEqual("a-b-c", new SplitJoinStringConverter("<>", "-").Convert("a<>b<>c"));

        // The limit counts parts, not separators, and the last part swallows the remainder with its
        // separators still in it — the same bargain string.Split(count) makes. Zero and anything
        // negative mean no limit, because the guard is "> 0"; a limit at or above the real part count
        // is indistinguishable from no limit.
        [TestCase(0, "a | b | c | d")]
        [TestCase(1, "a,b,c,d")]
        [TestCase(2, "a | b,c,d")]
        [TestCase(3, "a | b | c,d")]
        [TestCase(4, "a | b | c | d")]
        [TestCase(9, "a | b | c | d")]
        [TestCase(-1, "a | b | c | d")]
        public void Convert_MaxPartsCapsThePartCount(int maxParts, string expected) =>
            Assert.AreEqual(expected, new SplitJoinStringConverter(",", " | ", maxParts).Convert("a,b,c,d"));

        // Trimming applies to the ends of each part, and the capped last part is one part — so the
        // whitespace inside the swallowed remainder is left exactly as it arrived while the outer
        // edges still come off.
        [Test]
        public void Convert_TrimsTheEndsOfTheSwallowedRemainderOnly() =>
            Assert.AreEqual("a|b , c", new SplitJoinStringConverter(",", "|", maxParts: 2).Convert("a, b , c "));

        // The empty string is returned by the guard rather than walked, so it never becomes a single
        // empty part with a join around it.
        [Test]
        public void Convert_EmptyInputIsReturnedUnchanged() =>
            Assert.AreEqual(string.Empty, new SplitJoinStringConverter(",", " | ").Convert(string.Empty));

        [Test]
        public void Convert_NullInputIsReturnedUnchanged() =>
            Assert.IsNull(new SplitJoinStringConverter(",", " | ").Convert(null));

        // Whitespace is blank too, so it takes the same guard as the empty string rather than going
        // through the loop as one part trimmed to nothing: a blank label is preserved, not emptied.
        [Test]
        public void Convert_WhitespaceOnlyInputIsReturnedUnchanged()
        {
            const string value = "   ";

            Assert.AreSame(value, new SplitJoinStringConverter(",", " | ").Convert(value));
        }

        [Test]
        public void Convert_InputWithoutTheSeparatorIsOnePart() =>
            Assert.AreEqual("abc", new SplitJoinStringConverter(",", " | ").Convert("abc"));

        // Nothing to split on, so the value has to survive untouched — including the trimming, which
        // is skipped along with the rest of the walk.
        [Test]
        public void Convert_EmptySeparatorPassesTheValueThrough() =>
            Assert.AreEqual(" a,b ", new SplitJoinStringConverter(string.Empty, " | ").Convert(" a,b "));

        // An empty part is still a part: it gets its join and its place. Dropping them would be a
        // different converter.
        [TestCase("a,b,", "a | b | ")]
        [TestCase(",a,b", " | a | b")]
        [TestCase("a,,b", "a |  | b")]
        [TestCase(",", " | ")]
        public void Convert_EmptyPartsAreKept(string value, string expected) =>
            Assert.AreEqual(expected, new SplitJoinStringConverter(",", " | ").Convert(value));

        // "a, b ,c" and "a,b,c" have to produce the same thing, which is the whole point of the
        // default. Tabs count too, because the scan uses char.IsWhiteSpace rather than a space test.
        [TestCase("a, b ,c", "a|b|c")]
        [TestCase("a,\tb\t,c", "a|b|c")]
        [TestCase("a,   ,b", "a||b")]
        public void Convert_TrimsPartsByDefault(string value, string expected) =>
            Assert.AreEqual(expected, new SplitJoinStringConverter(",", "|").Convert(value));

        // _trimParts defaults to true and no constructor overload exposes it, so the untrimmed path
        // is reachable only from the Inspector — and therefore only from reflection here.
        [Test]
        public void Convert_TrimPartsOffKeepsTheWhitespaceAroundEachPart()
        {
            var converter = new SplitJoinStringConverter(",", "-");
            SetField(converter, "_trimParts", false);

            Assert.AreEqual("a- b -c", converter.Convert("a, b ,c"));
        }

        // The builder is cached on the instance; without the Clear() the second call would return
        // "a|bx|y".
        [Test]
        public void Convert_ReusingOneInstanceDoesNotAccumulate()
        {
            var converter = new SplitJoinStringConverter(",", "|");

            Assert.AreEqual("a|b", converter.Convert("a,b"));
            Assert.AreEqual("x|y", converter.Convert("x,y"), "the cached builder leaked the previous call");
        }

        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field.SetValue(target, value);

            if (target is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();
        }
    }
}
