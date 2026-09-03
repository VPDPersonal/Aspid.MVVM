using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionJoinToStringConverter{T}"/> — the separator, the per-item
    /// converter, the overflow trim, and the reused string builder.
    /// </summary>
    [TestFixture]
    public sealed class CollectionJoinToStringConverterTests
    {
        private static readonly string[] _three = { "a", "b", "c" };

        [Test]
        public void JoinToString_JoinsWithTheSeparator() =>
            Assert.AreEqual("a, b, c", new CollectionJoinToStringConverter<string>(", ").Convert(_three));

        // The item slot takes any converter, so the per-item text is not limited to a composite format.
        [Test]
        public void JoinToString_ItemConverter_WritesEachItem() =>
            Assert.AreEqual(
                "[a], [b], [c]",
                new CollectionJoinToStringConverter<string>(", ", item: new ValueToStringConverter<string>("[{0}]"))
                    .Convert(_three));

        [Test]
        public void JoinToString_TrimsAndReportsTheOverflow() =>
            Assert.AreEqual("a, b +1 more", new CollectionJoinToStringConverter<string>(", ", maxItems: 2).Convert(_three));

        [Test]
        public void JoinToString_EmptyUsesTheEmptyText() =>
            Assert.AreEqual("—", new CollectionJoinToStringConverter<string>(", ", 0, "—").Convert(Array.Empty<string>()));

        [Test]
        public void JoinToString_NullUsesTheEmptyText() =>
            Assert.AreEqual("—", new CollectionJoinToStringConverter<string>(", ", 0, "—").Convert(null));

        // The builder is reused between calls, so a second call must not see the first one's text.
        [Test]
        public void JoinToString_ReusedBuilderDoesNotLeakBetweenCalls()
        {
            var converter = new CollectionJoinToStringConverter<string>(", ");

            Assert.AreEqual("a, b, c", converter.Convert(_three));
            Assert.AreEqual("x", converter.Convert(new[] { "x" }));
        }
    }
}
