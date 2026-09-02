using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToSpriteConverter"/> — key matching, case sensitivity, blank
    /// keys, and misses.
    /// </summary>
    [TestFixture]
    public sealed class StringToSpriteConverterTests : SceneFixture
    {
        private const string MappedKey = "sword_iron";

        [Test]
        public void KeyInTheMap_ReturnsThatSprite()
        {
            var icon = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, icon) }, NewSprite());

            Assert.AreSame(icon, converter.Convert(MappedKey));
        }

        // The scan is linear and returns on the first match, so a map that names the same key twice
        // resolves to whichever entry is higher in the array rather than to the last word.
        [Test]
        public void DuplicateKeys_TakesTheFirstEntry()
        {
            var first = NewSprite();
            var second = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, first), Entry(MappedKey, second) });

            Assert.AreSame(first, converter.Convert(MappedKey));
        }

        [Test]
        public void IgnoreCase_MatchesADifferentlyCasedKey()
        {
            var icon = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, icon) }, NewSprite(), ignoreCase: true);

            Assert.AreSame(icon, converter.Convert(MappedKey.ToUpperInvariant()));
        }

        // The half that keeps the option honest: with it off the comparison is Ordinal, so a backend
        // that shouts its ids is a miss and not a silent match.
        [Test]
        public void CaseSensitiveByDefault_TreatsADifferentCaseAsAMiss()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToSpriteConverter.*a key the map holds.*SWORD_IRON"));

            var fallback = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, NewSprite()) }, fallback);

            Assert.AreSame(fallback, converter.Convert(MappedKey.ToUpperInvariant()));
        }

        // Every miss is reported, the same as everywhere else the failure modes are handled: a key that
        // starts missing halfway through a session is exactly the case a report-once rule hides.
        [Test]
        public void RepeatedMiss_ReportsEveryMiss()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToSpriteConverter.*a key the map holds"));
            LogAssert.Expect(LogType.Error, new Regex("StringToSpriteConverter.*a key the map holds"));
            LogAssert.Expect(LogType.Error, new Regex("StringToSpriteConverter.*a key the map holds"));

            var fallback = NewSprite();
            var converter = new StringToSpriteConverter(null, fallback);

            Assert.AreSame(fallback, converter.Convert("a"));
            Assert.AreSame(fallback, converter.Convert("b"));
            Assert.AreSame(fallback, converter.Convert("c"));
        }

        // A ViewModel with nothing selected pushes an empty id, which is a state and not a mistake.
        // No LogAssert.Expect here on purpose: an error would fail the test, and that is the
        // assertion — silence cannot be asserted any other way.
        [TestCase((string)null)]
        [TestCase("")]
        public void BlankKey_ReturnsTheFallbackWithoutReporting(string key)
        {
            var fallback = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, NewSprite()) }, fallback);

            Assert.AreSame(fallback, converter.Convert(key));
        }

        // A key of only spaces is blank, not a miss: it takes the fallback without a report, the same
        // as a null or empty one.
        [Test]
        public void WhitespaceKey_TakesTheFallbackSilently()
        {
            var fallback = NewSprite();

            Assert.AreSame(fallback, new StringToSpriteConverter(null, fallback).Convert(" "));
        }

        // "Leave it empty to map a key to nothing" is a hit, not a failure: it returns null instead
        // of falling through to the fallback, which is how a map hides an icon for one id.
        [Test]
        public void KeyMappedToNothing_ReturnsNullRatherThanTheFallback()
        {
            var converter = new StringToSpriteConverter(new[] { Entry("hidden", null) }, NewSprite());

            Assert.IsNull(converter.Convert("hidden"));
        }

        // Undocumented and invisible in the Inspector: an entry authored with an empty key can never
        // be reached, because the blank-input test returns the fallback before the scan begins.
        [Test]
        public void EntryWithAnEmptyKey_IsUnreachable()
        {
            var mapped = NewSprite();
            var fallback = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(string.Empty, mapped) }, fallback);

            Assert.AreSame(fallback, converter.Convert(string.Empty));
        }

        private static SpriteMapEntry Entry(string key, Sprite sprite) =>
            new(key, sprite);

        private Sprite NewSprite()
        {
            var texture = Track(new Texture2D(4, 4));
            return Track(Sprite.Create(texture, new Rect(0f, 0f, 4f, 4f), new Vector2(0.5f, 0.5f)));
        }
    }
}
