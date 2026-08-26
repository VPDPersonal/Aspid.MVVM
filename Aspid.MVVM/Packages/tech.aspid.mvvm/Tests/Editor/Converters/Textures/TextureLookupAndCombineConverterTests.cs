using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the texture converters the catalogue-wide fixture leaves alone —
    /// <see cref="StringToSpriteConverter"/> and <see cref="TextureToSpriteRectConverter"/> — plus
    /// the missing-target degrade of the 2D combine converters built on
    /// <see cref="Vector2CombineConverter"/>.
    /// </summary>
    /// <remarks>
    /// The mistakes here all look the same from the Inspector and differ only once something is running:
    /// a lookup quietly wrong for a key nobody typed the way the map spells it, a diagnostic firing per
    /// push muted after the first one, and an emptiness check written as <c>is null</c> — which sees a
    /// managed reference that outlived its native object, so a destroyed texture throws. Where code and
    /// XML docs disagree the code is what is pinned, and the comment on the case says so.
    /// </remarks>
    [TestFixture]
    internal sealed class TextureLookupAndCombineConverterTests
    {
        private const string MappedKey = "sword_iron";

        private readonly List<UnityEngine.Object> _created = new();

        [TearDown]
        public void DestroyCreatedObjects()
        {
            // Unity's implicit bool is false for the ones a test destroyed on purpose, and for a
            // sprite whose texture went first.
            foreach (var created in _created)
            {
                if (created) UnityEngine.Object.DestroyImmediate(created);
            }

            _created.Clear();
        }

        [Test]
        public void StringToSprite_KeyInTheMap_ReturnsThatSprite()
        {
            var icon = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, icon) }, NewSprite());

            Assert.AreSame(icon, converter.Convert(MappedKey));
        }

        // The scan is linear and returns on the first match, so a map that names the same key twice
        // resolves to whichever entry is higher in the array rather than to the last word.
        [Test]
        public void StringToSprite_DuplicateKeys_TakesTheFirstEntry()
        {
            var first = NewSprite();
            var second = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, first), Entry(MappedKey, second) });

            Assert.AreSame(first, converter.Convert(MappedKey));
        }

        [Test]
        public void StringToSprite_IgnoreCase_MatchesADifferentlyCasedKey()
        {
            var icon = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, icon) }, NewSprite(), ignoreCase: true);

            Assert.AreSame(icon, converter.Convert(MappedKey.ToUpperInvariant()));
        }

        // The half that keeps the option honest: with it off the comparison is Ordinal, so a backend
        // that shouts its ids is a miss and not a silent match.
        [Test]
        public void StringToSprite_CaseSensitiveByDefault_TreatsADifferentCaseAsAMiss()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToSpriteConverter.*a key the map holds.*SWORD_IRON"));

            var fallback = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, NewSprite()) }, fallback);

            Assert.AreSame(fallback, converter.Convert(MappedKey.ToUpperInvariant()));
        }

        // Every miss is reported, the same as everywhere else the failure modes are handled: a key that
        // starts missing halfway through a session is exactly the case a report-once rule hides.
        [Test]
        public void StringToSprite_RepeatedMiss_ReportsEveryMiss()
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
        public void StringToSprite_BlankKey_ReturnsTheFallbackWithoutReporting(string key)
        {
            var fallback = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(MappedKey, NewSprite()) }, fallback);

            Assert.AreSame(fallback, converter.Convert(key));
        }

        // A key of only spaces is blank, not a miss: it takes the fallback without a report, the same
        // as a null or empty one.
        [Test]
        public void StringToSprite_WhitespaceKey_TakesTheFallbackSilently()
        {
            var fallback = NewSprite();

            Assert.AreSame(fallback, new StringToSpriteConverter(null, fallback).Convert(" "));
        }

        // "Leave it empty to map a key to nothing" is a hit, not a failure: it returns null instead
        // of falling through to the fallback, which is how a map hides an icon for one id.
        [Test]
        public void StringToSprite_KeyMappedToNothing_ReturnsNullRatherThanTheFallback()
        {
            var converter = new StringToSpriteConverter(new[] { Entry("hidden", null) }, NewSprite());

            Assert.IsNull(converter.Convert("hidden"));
        }

        // Undocumented and invisible in the Inspector: an entry authored with an empty key can never
        // be reached, because the blank-input test returns the fallback before the scan begins.
        [Test]
        public void StringToSprite_EntryWithAnEmptyKey_IsUnreachable()
        {
            var mapped = NewSprite();
            var fallback = NewSprite();
            var converter = new StringToSpriteConverter(new[] { Entry(string.Empty, mapped) }, fallback);

            Assert.AreSame(fallback, converter.Convert(string.Empty));
        }

        [Test]
        public void TextureToSpriteRect_Texture_MeasuresTheWholePixelRect() =>
            Assert.AreEqual(new Rect(0f, 0f, 8f, 4f), new TextureToSpriteRectConverter().Convert(NewTexture(8, 4)));

        [Test]
        public void TextureToSpriteRect_Null_ReturnsZero() =>
            Assert.AreEqual(Rect.zero, new TextureToSpriteRectConverter().Convert(null));

        // The case the null check exists for. An asset unloaded under a bound RawImage leaves a live
        // managed reference behind, so `is null` and `??` both wave it through and the width read
        // throws inside the binder's push. Only Unity's overloaded == catches it.
        [Test]
        public void TextureToSpriteRect_DestroyedTexture_ReturnsZeroRatherThanThrowing()
        {
            var texture = NewTexture(8, 4);
            var converter = new TextureToSpriteRectConverter();

            // While it is alive it has to measure, or the zero below would prove nothing.
            Assert.AreEqual(new Rect(0f, 0f, 8f, 4f), converter.Convert(texture));

            UnityEngine.Object.DestroyImmediate(texture);

            Assert.AreEqual(Rect.zero, converter.Convert(texture));
        }

        // Typed on Texture rather than Texture2D so a render target measures the same way — the
        // reason a RawImage-facing ViewModel can hold the base type.
        [Test]
        public void TextureToSpriteRect_RenderTexture_MeasuresTheSameWay()
        {
            var texture = new RenderTexture(16, 8, 0);
            _created.Add(texture);

            Assert.AreEqual(new Rect(0f, 0f, 16f, 8f), new TextureToSpriteRectConverter().Convert(texture));
        }

        // The docs promise a Texture2D-typed binder still takes it. That holds only while IConverter
        // keeps its `in` on the input, so this assignment is the real assertion — losing the variance
        // annotation breaks the compile here rather than in a project's binder.
        [Test]
        public void TextureToSpriteRect_AssignedToATexture2DTypedConverter_StillMeasures()
        {
            IConverter<Texture2D, Rect> converter = new TextureToSpriteRectConverter();

            Assert.AreEqual(new Rect(0f, 0f, 2f, 2f), converter.Convert(NewTexture(2, 2)));
        }

        // An unassigned Inspector reference is the normal state of a half-built prefab. Reading the
        // collider's offset would throw and take every binder queued behind this one down with it.
        [Test]
        public void BoxCollider2DOffsetCombine_MissingTarget_ReturnsTheInputRatherThanThrowing()
        {
            // Named in full: the message has to say which converter is empty, and GetType().Name is
            // what makes it the subclass rather than the shared base.
            LogAssert.Expect(LogType.Error, new Regex("BoxCollider2DOffsetCombineConverter.*no target assigned"));

            var value = new Vector2(1f, 2f);

            Assert.AreEqual(value, new BoxCollider2DOffsetCombineConverter().Convert(value));
        }

        // Every push, not once per converter: an empty Inspector reference stays loud until somebody
        // fills it in. LogAssert fails the test on any error the fixture did not ask for, so three
        // expectations against three pushes is the assertion that the diagnostic is not muted after
        // the first one.
        [Test]
        public void BoxCollider2DOffsetCombine_MissingTarget_ReportsOnEveryPush()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            var converter = new BoxCollider2DOffsetCombineConverter();
            converter.Convert(new Vector2(1f, 2f));
            converter.Convert(new Vector2(3f, 4f));
            converter.Convert(new Vector2(5f, 6f));
        }

        // The Vector3 entry point degrades to the *narrowed* input: z is dropped at the call, so what
        // comes back is not the value that was pushed.
        [Test]
        public void BoxCollider2DOffsetCombine_Vector3_MissingTarget_ReturnsTheInputWithoutItsZ()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            Assert.AreEqual(
                new Vector2(1f, 2f),
                new BoxCollider2DOffsetCombineConverter().Convert(new Vector3(1f, 2f, 3f)));
        }

        // The pair below is what stops the degrade assertion from being vacuous. Every shipped 2D
        // converter keeps its target in a private [SerializeField] and exposes no mode, so a live
        // target and a mode other than XY can only come from a stub.
        [Test]
        public void Vector2Combine_ModeX_LiveTarget_TakesYFromTheReferenceVector()
        {
            var target = NewGameObject(nameof(Vector2Combine_ModeX_LiveTarget_TakesYFromTheReferenceVector));
            var converter = new CombineStub(target.transform, new Vector2(10f, 20f), Vector2CombineConverter.Mode.X);

            Assert.AreEqual(new Vector2(1f, 20f), converter.Convert(new Vector2(1f, 2f)));
        }

        // Same mode, no target: the y that a live reference would have supplied stays as it arrived,
        // so "returns the input" is a genuine degrade and not the identity XY gives for free.
        [Test]
        public void Vector2Combine_ModeX_MissingTarget_KeepsTheInputY()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            var converter = new CombineStub(null, new Vector2(10f, 20f), Vector2CombineConverter.Mode.X);

            Assert.AreEqual(new Vector2(1f, 2f), converter.Convert(new Vector2(1f, 2f)));
        }

        private static SpriteMapEntry Entry(string key, Sprite sprite) =>
            new(key, sprite);

        private Sprite NewSprite()
        {
            var texture = NewTexture(4, 4);
            var sprite = Sprite.Create(texture, new Rect(0f, 0f, 4f, 4f), new Vector2(0.5f, 0.5f));
            _created.Add(sprite);

            return sprite;
        }

        private Texture2D NewTexture(int width, int height)
        {
            var texture = new Texture2D(width, height);
            _created.Add(texture);

            return texture;
        }

        // Hidden and unsaved so that a failing assertion cannot leave a stray object in the editor scene.
        private GameObject NewGameObject(string name)
        {
            var gameObject = new GameObject(name) { hideFlags = HideFlags.HideAndDontSave };
            _created.Add(gameObject);

            return gameObject;
        }

        private sealed class CombineStub : Vector2CombineConverter
        {
            private readonly Vector2 _to;
            private readonly Component _target;

            public CombineStub(Component target, Vector2 to, Mode mode)
                : base(mode)
            {
                _target = target;
                _to = to;
            }

            protected override Component Target => _target;

            // Throws when there is no target, so a guard that ran after the read would fail here
            // instead of passing by accident.
            protected override Vector2 VectorTo =>
                _target == null ? throw new NullReferenceException(nameof(VectorTo)) : _to;
        }
    }
}
