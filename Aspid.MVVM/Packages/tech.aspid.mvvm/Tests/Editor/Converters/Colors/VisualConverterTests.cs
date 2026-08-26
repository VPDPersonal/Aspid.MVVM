using UnityEngine;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the color, ColorBlock and texture converters.
    /// </summary>
    /// <remarks>
    /// This wave fills the two pickers that had been declared but never implemented, so a few of the
    /// assertions are really about the field being usable at all rather than about arithmetic.
    /// </remarks>
    [TestFixture]
    internal sealed class VisualConverterTests
    {
        [Test]
        public void ColorAlpha_SetsTheAlphaAndLeavesTheHue()
        {
            var result = new ColorAlphaConverter(0.5f).Convert(new Color(0.2f, 0.4f, 0.6f, 1f));

            Assert.AreEqual(0.5f, result.a, 1e-5f);
            Assert.AreEqual(0.2f, result.r, 1e-5f);
        }

        [Test]
        public void ColorAlpha_Multiplies() =>
            Assert.AreEqual(
                0.25f,
                new ColorAlphaConverter(0.5f, AlphaMode.Multiply).Convert(new Color(1f, 1f, 1f, 0.5f)).a,
                1e-5f);

        // The [Range] screens the serialized field but not a constructor argument, and the two other
        // modes clamp already — Set holding to 0..1 is what makes the three agree on their output.
        [TestCase(2f, 1f)]
        [TestCase(-1f, 0f)]
        public void ColorAlpha_Set_HoldsTheAlphaToTheZeroOneRange(float alpha, float expected) =>
            Assert.AreEqual(
                expected,
                new ColorAlphaConverter(alpha).Convert(new Color(0.2f, 0.4f, 0.6f, 0.5f)).a,
                1e-5f);

        // The hue is left alone whatever happens to the alpha, so an unchanged 0.5 alpha alongside
        // the original hue is the only reading that says the mode was skipped rather than applied.
        [Test]
        public void ColorAlpha_UndeclaredMode_ReportsItAndLeavesTheAlphaAlone()
        {
            LogAssert.Expect(LogType.Error, new Regex("ColorAlphaConverter.*not a declared AlphaMode"));

            var result = new ColorAlphaConverter(0.25f, (AlphaMode)42).Convert(new Color(0.2f, 0.4f, 0.6f, 0.5f));

            Assert.AreEqual(0.5f, result.a, 1e-5f);
            Assert.AreEqual(0.2f, result.r, 1e-5f);
        }

        [Test]
        public void ColorTint_Multiplies() =>
            Assert.AreEqual(
                new Color(0.5f, 0f, 0f, 1f),
                new ColorTintConverter(Color.red).Convert(new Color(0.5f, 0.5f, 0.5f, 1f)));

        [Test]
        public void ColorTint_ReplaceKeepsTheOriginalAlpha()
        {
            var result = new ColorTintConverter(Color.red, ColorBlend.Replace).Convert(new Color(0f, 0f, 1f, 0.3f));

            Assert.AreEqual(1f, result.r, 1e-5f);
            Assert.AreEqual(0.3f, result.a, 1e-5f);
        }

        [Test]
        public void ColorGrayscale_UsesLuminanceWeightsNotAFlatAverage()
        {
            // A flat average of pure green would be 0.333; the eye reads it as 0.587.
            var result = new ColorGrayscaleConverter(0f).Convert(Color.green);

            Assert.AreEqual(0.587f, result.r, 1e-3f);
            Assert.AreEqual(result.r, result.g, 1e-6f);
            Assert.AreEqual(result.r, result.b, 1e-6f);
        }

        [Test]
        public void ColorGrayscale_KeepsAlpha() =>
            Assert.AreEqual(0.4f, new ColorGrayscaleConverter(0f).Convert(new Color(1f, 0f, 0f, 0.4f)).a, 1e-5f);

        [Test]
        public void ColorHsv_HalfATurnGivesTheOppositeHue()
        {
            var result = new ColorHsvConverter(0.5f).Convert(Color.red);

            Color.RGBToHSV(result, out var hue, out _, out _);
            Assert.AreEqual(0.5f, hue, 1e-3f);
        }

        [Test]
        public void ColorToHtmlString_WritesTheHex() =>
            Assert.AreEqual("#FF0000", new ColorToHtmlStringConverter().Convert(Color.red));

        [Test]
        public void ColorToHtmlString_RoundTripsThroughParseHtmlString()
        {
            var text = new ColorToHtmlStringConverter(includeAlpha: true).Convert(new Color(0.2f, 0.4f, 0.6f, 0.8f));
            var parsed = new ParseHtmlStringConverter().Convert(text);

            Assert.AreEqual(0.2f, parsed.r, 0.01f);
            Assert.AreEqual(0.8f, parsed.a, 0.01f);
        }

        [TestCase("#FF0000")]
        [TestCase("red")]
        public void ColorToHtmlString_ConvertBack_ParsesAnHtmlColor(string html) =>
            Assert.AreEqual(Color.red, new ColorToHtmlStringConverter().ConvertBack(html));

        [Test]
        public void ColorToHtmlString_ConvertBack_RoundTripsItsOwnOutput()
        {
            var converter = new ColorToHtmlStringConverter(includeAlpha: true);
            var parsed = converter.ConvertBack(converter.Convert(new Color(0.2f, 0.4f, 0.6f, 0.8f)));

            Assert.AreEqual(0.2f, parsed.r, 0.01f);
            Assert.AreEqual(0.8f, parsed.a, 0.01f);
        }

        // A blank string is no value rather than a failed parse, in this direction too.
        [TestCase((string)null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ColorToHtmlString_ConvertBack_BlankString_ReturnsTheFallbackSilently(string html)
        {
            var converter = new ColorToHtmlStringConverter(includeAlpha: true, convertBackFallback: Color.magenta);

            Assert.AreEqual(Color.magenta, converter.ConvertBack(html));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void ColorToHtmlString_ConvertBack_UnparseableString_ReturnsTheFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(LogType.Error, new Regex("ColorToHtmlStringConverter.*an HTML color"));

            var converter = new ColorToHtmlStringConverter(includeAlpha: true, convertBackFallback: Color.magenta);

            Assert.AreEqual(Color.magenta, converter.ConvertBack("not a colour"));
            converter.ConvertBack("still not");
            converter.ConvertBack("nor this");
        }

        // The two converters share one parse path, so the reverse direction answers exactly as
        // ParseHtmlStringConverter.Convert does — down to the default fallback.
        [Test]
        public void ColorToHtmlString_ConvertBack_WithoutAFallback_AnswersLikeParseHtmlString()
        {
            LogAssert.Expect(LogType.Error, new Regex("ParseHtmlStringConverter.*an HTML color"));
            var expected = new ParseHtmlStringConverter().Convert("nope");

            LogAssert.Expect(LogType.Error, new Regex("ColorToHtmlStringConverter.*an HTML color"));
            Assert.AreEqual(expected, new ColorToHtmlStringConverter().ConvertBack("nope"));
        }

        [Test]
        public void GradientEvaluate_ReadsTheRamp()
        {
            var gradient = new Gradient();
            gradient.SetKeys(
                new[] { new GradientColorKey(Color.red, 0f), new GradientColorKey(Color.green, 1f) },
                new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });

            var converter = new GradientEvaluateConverter(gradient);

            Assert.AreEqual(Color.red, converter.Convert(0f));
            Assert.AreEqual(Color.green, converter.Convert(1f));
        }

        [Test]
        public void GradientEvaluate_MapsTheInputRange()
        {
            var gradient = new Gradient();
            gradient.SetKeys(
                new[] { new GradientColorKey(Color.red, 0f), new GradientColorKey(Color.green, 1f) },
                new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });

            Assert.AreEqual(Color.green, new GradientEvaluateConverter(gradient, 0f, 100f).Convert(100f));
        }

        [Test]
        public void ColorLerp_MovesBetweenTheStops()
        {
            var converter = new ColorLerpConverter(Color.red, Color.green);

            Assert.AreEqual(Color.red, converter.Convert(0f));
            Assert.AreEqual(Color.green, converter.Convert(1f));
        }

        [Test]
        public void ThresholdColor_PicksTheHighestQualifyingStop()
        {
            var converter = new ThresholdColorConverter(
                new[]
                {
                    new ColorStop(0.75f, Color.green),
                    new ColorStop(0.25f, Color.blue),
                },
                fallback: Color.red);

            Assert.AreEqual(Color.green, converter.Convert(0.9f));
            Assert.AreEqual(Color.blue, converter.Convert(0.5f));
            Assert.AreEqual(Color.red, converter.Convert(0.1f));
        }

        // string.GetHashCode is randomised per process, so the same name would take a different
        // color on every launch. The hash here is FNV-1a, which is not.
        [Test]
        public void HashToColor_IsStableForTheSameName()
        {
            var converter = new HashToColorConverter();

            Assert.AreEqual(converter.Convert("Vladislav"), converter.Convert("Vladislav"));
            Assert.AreNotEqual(converter.Convert("Vladislav"), converter.Convert("Someone"));
        }

        [Test]
        public void HashToColor_BlankGivesTheFallback() =>
            Assert.AreEqual(Color.gray, new HashToColorConverter().Convert(null));

        [Test]
        public void HashToColor_AuthoredFallback_IsUsedForABlankName() =>
            Assert.AreEqual(
                Color.magenta,
                new HashToColorConverter(0.6f, fallback: Color.magenta).Convert(string.Empty));

        // The Range attribute only holds the Inspector; the constructor takes any float, and HSVToRGB
        // reads a saturation above one as a color outside the gamut.
        [Test]
        public void HashToColor_SaturationOutsideTheRange_IsHeldToIt() =>
            Assert.AreEqual(
                new HashToColorConverter(1f).Convert("Vladislav"),
                new HashToColorConverter(4f).Convert("Vladislav"));

        [Test]
        public void ColorToColorBlock_DerivesEveryState()
        {
            var block = new ColorToColorBlockConverter().Convert(Color.white);

            Assert.AreEqual(Color.white, block.normalColor);
            Assert.Less(block.pressedColor.r, block.normalColor.r);
            Assert.AreEqual(0.5f, block.disabledColor.a, 1e-5f);
        }

        [Test]
        public void ColorToColorBlock_AuthoredMultipliers_ScaleEachState()
        {
            var block = new ColorToColorBlockConverter(0.8f, pressedMultiplier: 0.4f, disabledAlpha: 0.2f)
                .Convert(Color.white);

            Assert.AreEqual(0.8f, block.highlightedColor.r, 1e-5f);
            Assert.AreEqual(0.4f, block.pressedColor.r, 1e-5f);
            Assert.AreEqual(0.2f, block.disabledColor.a, 1e-5f);
        }

        // UGUI renders a Selectable black at a zero multiplier, and the [Range] screens the field but
        // not the constructor argument. Held to the near end rather than to the default 1, so a value
        // above the range keeps the emphasis it was asking for.
        [TestCase(0f, 1f)]
        [TestCase(9f, 5f)]
        [TestCase(float.NaN, 1f)]
        public void ColorToColorBlock_MultiplierOutsideTheRange_ReportsItAndHoldsItToTheRange(
            float authored,
            float expected)
        {
            LogAssert.Expect(LogType.Error, new Regex("ColorToColorBlockConverter.*color multiplier"));

            var block = new ColorToColorBlockConverter(1.1f, colorMultiplier: authored).Convert(Color.white);

            Assert.AreEqual(expected, block.colorMultiplier, 1e-5f);
        }

        [Test]
        public void ColorBlockTint_TintsEveryState()
        {
            var block = new ColorToColorBlockConverter().Convert(Color.white);
            var tinted = new ColorBlockTintConverter(Color.red).Convert(block);

            Assert.AreEqual(0f, tinted.normalColor.g, 1e-5f);
            Assert.AreEqual(0f, tinted.pressedColor.g, 1e-5f);
            Assert.AreEqual(0f, tinted.disabledColor.g, 1e-5f);
        }

        [Test]
        public void ColorBlockAlpha_DimsEveryState()
        {
            var block = ColorBlock.defaultColorBlock;
            var dimmed = new ColorBlockAlphaConverter(0.5f).Convert(block);

            Assert.AreEqual(block.normalColor.a * 0.5f, dimmed.normalColor.a, 1e-5f);
            Assert.AreEqual(block.highlightedColor.a * 0.5f, dimmed.highlightedColor.a, 1e-5f);
        }

        [Test]
        public void ColorBlockFadeDuration_SetsOnlyTheDuration()
        {
            var block = ColorBlock.defaultColorBlock;
            var slowed = new ColorBlockFadeDurationConverter(0.5f).Convert(block);

            Assert.AreEqual(0.5f, slowed.fadeDuration, 1e-5f);
            Assert.AreEqual(block.normalColor, slowed.normalColor);
        }

        // A Selectable tween over a negative duration never finishes, so the state change is stuck
        // rather than instant — and nothing about the block says which duration it was handed.
        [Test]
        public void ColorBlockFadeDuration_NegativeDuration_IsReportedAndFadesInstantly()
        {
            LogAssert.Expect(LogType.Error, new Regex("ColorBlockFadeDurationConverter.*not a length of time"));

            var slowed = new ColorBlockFadeDurationConverter(-1f).Convert(ColorBlock.defaultColorBlock);

            Assert.AreEqual(0f, slowed.fadeDuration, 1e-5f);
        }

        [Test]
        public void SpriteToTexture_ReadsTheTexture()
        {
            var texture = new Texture2D(4, 4);
            var sprite = Sprite.Create(texture, new Rect(0, 0, 4, 4), Vector2.one * 0.5f);

            try
            {
                Assert.AreSame(texture, new SpriteToTextureConverter().Convert(sprite));
                Assert.IsNull(new SpriteToTextureConverter().Convert(null));
            }
            finally
            {
                Object.DestroyImmediate(sprite);
                Object.DestroyImmediate(texture);
            }
        }

        // Sprite.Create allocates every call and a binder pushes on every notification, so without
        // the cache a bound avatar leaks a sprite per frame.
        [Test]
        public void Texture2DToSprite_ReusesTheSpriteWhileTheTextureIsUnchanged()
        {
            var texture = new Texture2D(4, 4);
            var converter = new Texture2DToSpriteConverter();

            try
            {
                var first = converter.Convert(texture);
                var second = converter.Convert(texture);

                Assert.IsNotNull(first);
                Assert.AreSame(first, second);
            }
            finally
            {
                Object.DestroyImmediate(texture);
            }
        }

        [Test]
        public void Texture2DToSprite_NullClearsTheCache() =>
            Assert.IsNull(new Texture2DToSpriteConverter().Convert(null));

        // Sprite.Create divides the pixel rect by this, so zero would hand back a sprite of infinite
        // world size — a bound image that simply stops drawing, with nothing in the log about it.
        [Test]
        public void Texture2DToSprite_PixelsPerUnitNotAboveZero_IsReportedAndBuildsAt100()
        {
            LogAssert.Expect(LogType.Error, new Regex("Texture2DToSpriteConverter.*not a scale"));

            var texture = new Texture2D(4, 4);
            var converter = new Texture2DToSpriteConverter(Vector2.one * 0.5f, 0f);

            try
            {
                var sprite = converter.Convert(texture);

                Assert.IsNotNull(sprite);
                Assert.AreEqual(100f, sprite.pixelsPerUnit, 1e-5f);
            }
            finally
            {
                Object.DestroyImmediate(texture);
            }
        }

        [Test]
        public void NormalizedToSprite_PicksTheFrame()
        {
            var texture = new Texture2D(2, 2);
            var frames = new[]
            {
                Sprite.Create(texture, new Rect(0, 0, 2, 2), Vector2.one * 0.5f),
                Sprite.Create(texture, new Rect(0, 0, 2, 2), Vector2.one * 0.5f),
            };

            try
            {
                var converter = new NormalizedToSpriteConverter(frames);

                Assert.AreSame(frames[0], converter.Convert(0f));
                Assert.AreSame(frames[1], converter.Convert(1f));
                Assert.AreSame(frames[1], converter.Convert(0.9f));
            }
            finally
            {
                foreach (var frame in frames) Object.DestroyImmediate(frame);
                Object.DestroyImmediate(texture);
            }
        }

        // A converter with no frames can never show anything, so it reports the misconfiguration on
        // every push rather than quietly answering null for the life of the scene.
        [Test]
        public void NormalizedToSprite_WithNoFramesReturnsNull()
        {
            LogAssert.Expect(LogType.Error, new Regex("NormalizedToSpriteConverter.*no frames are assigned"));

            Assert.IsNull(new NormalizedToSpriteConverter(null).Convert(0.5f));
        }

        [Test]
        public void ObjectName_StripsTheCloneSuffix()
        {
            var gameObject = new GameObject("Enemy(Clone)");

            try
            {
                Assert.AreEqual("Enemy", new ObjectNameConverter(fallback: string.Empty).Convert(gameObject));
            }
            finally
            {
                Object.DestroyImmediate(gameObject);
            }
        }

        [Test]
        public void ObjectName_MissingObjectGivesTheFallback() =>
            Assert.AreEqual("—", new ObjectNameConverter(fallback: "—").Convert(null));
    }
}
