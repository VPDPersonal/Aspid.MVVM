using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.UI;
using System.Reflection;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the <see cref="SelectableStates"/> mask on <see cref="ColorBlockTintConverter"/>
    /// and <see cref="ColorBlockStateConverter"/> — which of the five state colours each writes, and
    /// that the four it does not write survive bit for bit.
    /// </summary>
    /// <remarks>
    /// A mask converter fails in two ways a "does it tint?" test never sees: it writes a state the
    /// mask excluded, or it reads the wrong field and writes the right colour into the wrong state.
    /// Both need a source block whose five colours are all different, which is why nothing here
    /// starts from <see cref="ColorBlock.defaultColorBlock"/> — that block gives highlighted and
    /// selected the same colour, so a converter swapping the two would pass.
    /// <para>
    /// The second thing guarded is the default. The mask arrived after the converters shipped, so
    /// <see cref="SelectableStates.All"/> has to stay the default in both the constructor and the
    /// field initializer; any narrower default silently stops tinting a state that used to be
    /// tinted, and nothing about the binder would look broken.
    /// </para>
    /// <para>
    /// Neither converter logs or allocates a Unity object, so there is no <c>LogAssert</c> and
    /// nothing to destroy — <see cref="ColorBlock"/> is a struct and both converters are pure.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ColorBlockAdditionsTests
    {
        private const float Tolerance = 1e-6f;

        /// <summary>The five single-flag states, in the order <c>Convert</c> writes them.</summary>
        private static readonly SelectableStates[] EveryState =
        {
            SelectableStates.Normal,
            SelectableStates.Highlighted,
            SelectableStates.Pressed,
            SelectableStates.Selected,
            SelectableStates.Disabled,
        };

        /// <summary>Half grey at quarter alpha: every channel is a power of two, so a multiply is exact.</summary>
        private static readonly Color Tint = new(0.5f, 0.5f, 0.5f, 0.25f);

        /// <summary>The colour <see cref="ColorBlockStateConverter"/> writes — not grey, not any authored state.</summary>
        private static readonly Color Written = new(0.25f, 0.75f, 0.125f, 0.375f);

        // ---- ColorBlockTintConverter: the default mask ----

        // The expected column is the authored state multiplied by the tint, written out rather than
        // computed, so a converter that stopped multiplying alpha cannot agree with the test.
        [TestCase(SelectableStates.Normal, 0.5f, 0f, 0f, 0.25f)]
        [TestCase(SelectableStates.Highlighted, 0f, 0.5f, 0f, 0.225f)]
        [TestCase(SelectableStates.Pressed, 0f, 0f, 0.5f, 0.2f)]
        [TestCase(SelectableStates.Selected, 0.5f, 0.5f, 0f, 0.175f)]
        [TestCase(SelectableStates.Disabled, 0f, 0.5f, 0.5f, 0.15f)]
        public void TintConverter_DefaultMask_MultipliesEveryStateIncludingDisabled(
            SelectableStates state,
            float r,
            float g,
            float b,
            float a)
        {
            var result = new ColorBlockTintConverter(Tint).Convert(Authored());

            AssertSameColor(new Color(r, g, b, a), ColorOf(result, state), $"{state}");
        }

        [Test]
        public void TintConverter_DefaultedStatesArgument_MatchesAnExplicitAllMask()
        {
            var defaulted = new ColorBlockTintConverter(Tint, ColorBlend.Multiply).Convert(Authored());
            var explicitAll = new ColorBlockTintConverter(Tint, ColorBlend.Multiply, SelectableStates.All).Convert(Authored());

            foreach (var state in EveryState)
                Assert.AreEqual(ColorOf(explicitAll, state), ColorOf(defaulted, state), $"{state} differs from an explicit All mask.");
        }

        // The parameterless converter tints with white, which is an identity whatever the mask is, so
        // Convert cannot show what the mask defaults to. The field initializer is what a converter
        // picked in the Inspector gets, and it is the only place that default is written down.
        [Test]
        public void TintConverter_FieldInitializer_DefaultsToAll()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var field = typeof(ColorBlockTintConverter).GetField("_states", flags);
            if (field is null) throw new InvalidOperationException("ColorBlockTintConverter has no _states field.");

            Assert.AreEqual(SelectableStates.All, field.GetValue(new ColorBlockTintConverter()));
        }

        [Test]
        public void TintConverter_ParameterlessConstructor_IsAnIdentity()
        {
            var result = new ColorBlockTintConverter().Convert(Authored());

            foreach (var state in EveryState)
                Assert.AreEqual(ColorOf(Authored(), state), ColorOf(result, state), $"{state} was changed by a converter that should change nothing.");
        }

        // ---- ColorBlockTintConverter: what the mask spares ----

        [Test]
        public void TintConverter_InteractiveMask_LeavesTheDisabledStateExactlyAsAuthored()
        {
            var result = new ColorBlockTintConverter(Tint, ColorBlend.Multiply, SelectableStates.Interactive).Convert(Authored());

            // Bit-identical rather than close: a theme pushed on every notification would drift a
            // "nearly untouched" disabled colour a little further off with each push.
            Assert.AreEqual(Authored().disabledColor, result.disabledColor, "Disabled sits outside the Interactive mask.");

            foreach (var state in EveryState)
            {
                if (state == SelectableStates.Disabled) continue;

                Assert.AreNotEqual(ColorOf(Authored(), state), ColorOf(result, state), $"{state} sits inside Interactive and should have been tinted.");
            }
        }

        [TestCase(SelectableStates.Normal, 0.25f)]
        [TestCase(SelectableStates.Highlighted, 0.225f)]
        [TestCase(SelectableStates.Pressed, 0.2f)]
        [TestCase(SelectableStates.Selected, 0.175f)]
        [TestCase(SelectableStates.Disabled, 0.15f)]
        public void TintConverter_SingleStateMask_WritesOnlyThatState(SelectableStates state, float tintedAlpha)
        {
            var result = new ColorBlockTintConverter(Tint, ColorBlend.Multiply, state).Convert(Authored());

            Assert.AreEqual(tintedAlpha, ColorOf(result, state).a, Tolerance, $"{state} sits inside the mask and should have been tinted.");
            AssertUntouchedOutside(state, result);
        }

        // Same tint that changes all five under All, so this pins the mask rather than the colour.
        [Test]
        public void TintConverter_NoneMask_ChangesNothing()
        {
            var result = new ColorBlockTintConverter(Tint, ColorBlend.Multiply, SelectableStates.None).Convert(Authored());

            AssertUntouchedOutside(SelectableStates.None, result);
        }

        // The converter owns neither field, so it can only keep them by returning the block it was
        // given. A rewrite that builds a fresh ColorBlock would reset the fade to zero and the
        // multiplier to zero, and a Selectable with a zero multiplier renders black.
        [Test]
        public void TintConverter_LeavesFadeDurationAndMultiplierAlone()
        {
            var result = new ColorBlockTintConverter(Tint).Convert(Authored());

            Assert.AreEqual(Authored().fadeDuration, result.fadeDuration, Tolerance, "fadeDuration");
            Assert.AreEqual(Authored().colorMultiplier, result.colorMultiplier, Tolerance, "colorMultiplier");
        }

        // ---- ColorBlockTintConverter: the blends, reached through the mask ----

        // Add clamps the channels and keeps the authored alpha, where Multiply scales it — the same
        // tint therefore leaves this state fully opaque and the multiplied one at a quarter alpha.
        [Test]
        public void TintConverter_AddBlend_ClampsAndKeepsTheAuthoredAlpha()
        {
            var result = new ColorBlockTintConverter(Tint, ColorBlend.Add, SelectableStates.Normal).Convert(Authored());

            AssertSameColor(new Color(1f, 0.5f, 0.5f, 1f), result.normalColor, "Normal, added");
            AssertUntouchedOutside(SelectableStates.Normal, result);
        }

        // ColorTintConverter takes the Lerp amount as a constructor argument; this one does not, so
        // code-constructed Lerp always travels the whole way — including to the tint's own alpha,
        // which is what separates it from Replace.
        [Test]
        public void TintConverter_LerpBlend_TravelsAllTheWayBecauseTheConstructorHasNoAmount()
        {
            var result = new ColorBlockTintConverter(Tint, ColorBlend.Lerp, SelectableStates.Highlighted).Convert(Authored());

            AssertSameColor(Tint, result.highlightedColor, "Highlighted, lerped");
            Assert.AreNotEqual(Authored().highlightedColor.a, result.highlightedColor.a, "Lerp should have taken the tint's alpha.");
        }

        [Test]
        public void TintConverter_ReplaceBlend_KeepsTheAuthoredAlpha()
        {
            var result = new ColorBlockTintConverter(Tint, ColorBlend.Replace, SelectableStates.Pressed).Convert(Authored());

            AssertSameColor(new Color(Tint.r, Tint.g, Tint.b, Authored().pressedColor.a), result.pressedColor, "Pressed, replaced");
        }

        // An undeclared blend throws rather than passing the colour through, and the mask decides
        // whether the throw happens at all — the states are filtered before the blend runs, not
        // after. A converter that blended first and filtered second would throw for both masks.
        [Test]
        public void TintConverter_UndeclaredBlend_ThrowsOnlyForStatesInsideTheMask()
        {
            const ColorBlend undeclared = (ColorBlend)99;

            Assert.Throws<ArgumentOutOfRangeException>(
                () => new ColorBlockTintConverter(Tint, undeclared, SelectableStates.All).Convert(Authored()));

            Assert.DoesNotThrow(
                () => new ColorBlockTintConverter(Tint, undeclared, SelectableStates.None).Convert(Authored()));
        }

        // ---- ColorBlockStateConverter ----

        // ColorChannelConverter defaults to a multiply by white so that a freshly picked converter
        // passes the bound value through; this one does not follow that rule. Its defaults are
        // Disabled plus grey, so picking it in the Inspector already recolours a state before
        // anything is authored — deliberate, since that is the state it exists to pin, but the
        // asymmetry is what this pins down.
        [Test]
        public void StateConverter_ParameterlessConstructor_WritesGrayIntoDisabledOnly()
        {
            var result = new ColorBlockStateConverter().Convert(Authored());

            Assert.AreEqual(Color.gray, result.disabledColor, "The default converter should write grey into Disabled.");
            AssertUntouchedOutside(SelectableStates.Disabled, result);
        }

        [TestCase(SelectableStates.Normal)]
        [TestCase(SelectableStates.Highlighted)]
        [TestCase(SelectableStates.Pressed)]
        [TestCase(SelectableStates.Selected)]
        [TestCase(SelectableStates.Disabled)]
        public void StateConverter_SingleStateMask_WritesThatStateAndLeavesTheOtherFour(SelectableStates state)
        {
            var result = new ColorBlockStateConverter(state, Written).Convert(Authored());

            Assert.AreEqual(Written, ColorOf(result, state), $"{state} sits inside the mask and should carry the authored colour.");
            AssertUntouchedOutside(state, result);
        }

        // "This toggle stays lit once chosen": one converter pinning two states to one colour is the
        // reason the field is a mask rather than a single choice.
        [Test]
        public void StateConverter_CombinedMask_PinsBothStatesToTheSameColour()
        {
            const SelectableStates mask = SelectableStates.Normal | SelectableStates.Selected;

            var result = new ColorBlockStateConverter(mask, Written).Convert(Authored());

            Assert.AreEqual(Written, result.normalColor, "Normal sits inside the mask.");
            Assert.AreEqual(Written, result.selectedColor, "Selected sits inside the mask.");
            AssertUntouchedOutside(mask, result);
        }

        [Test]
        public void StateConverter_AllMask_FlattensEveryState()
        {
            var result = new ColorBlockStateConverter(SelectableStates.All, Written).Convert(Authored());

            foreach (var state in EveryState)
                Assert.AreEqual(Written, ColorOf(result, state), $"{state} sits inside the All mask.");
        }

        [Test]
        public void StateConverter_NoneMask_ChangesNothing()
        {
            var result = new ColorBlockStateConverter(SelectableStates.None, Written).Convert(Authored());

            AssertUntouchedOutside(SelectableStates.None, result);
        }

        [Test]
        public void StateConverter_LeavesFadeDurationAndMultiplierAlone()
        {
            var result = new ColorBlockStateConverter(SelectableStates.All, Written).Convert(Authored());

            Assert.AreEqual(Authored().fadeDuration, result.fadeDuration, Tolerance, "fadeDuration");
            Assert.AreEqual(Authored().colorMultiplier, result.colorMultiplier, Tolerance, "colorMultiplier");
        }

        // The documented pairing: a theme tints the whole block, then this puts back the one state
        // the theme had no business recolouring. The order matters — the state converter has to run
        // second, or the tint multiplies the authored colour it just wrote.
        [Test]
        public void StateConverter_ChainedAfterTheTint_RestoresTheDisabledColourTheThemeTook()
        {
            var themed = new ColorBlockTintConverter(Tint).Convert(Authored());
            var result = new ColorBlockStateConverter(SelectableStates.Disabled, Written).Convert(themed);

            Assert.AreEqual(Written, result.disabledColor, "The disabled colour should be the authored one, not the tinted one.");
            AssertSameColor(new Color(0.5f, 0f, 0f, 0.25f), result.normalColor, "Normal keeps the theme");
        }

        // ---- The converter next door, which has no mask ----

        // ColorBlockAlphaConverter writes all five states unconditionally, so it cannot spare the
        // disabled one the way the two masked converters can. Asserted rather than assumed: this
        // fails the day a mask is added with anything but All behind it.
        [Test]
        public void AlphaConverter_HasNoMask_SoItDimsTheDisabledStateToo()
        {
            var result = new ColorBlockAlphaConverter(0.5f, AlphaMode.Set).Convert(Authored());

            AssertSameColor(new Color(0f, 1f, 1f, 0.5f), result.disabledColor, "Disabled, dimmed");
        }

        /// <summary>
        /// A block whose five state colours are all different, none of them grey, white or the tint.
        /// </summary>
        private static ColorBlock Authored() => new()
        {
            normalColor = new Color(1f, 0f, 0f, 1f),
            highlightedColor = new Color(0f, 1f, 0f, 0.9f),
            pressedColor = new Color(0f, 0f, 1f, 0.8f),
            selectedColor = new Color(1f, 1f, 0f, 0.7f),
            disabledColor = new Color(0f, 1f, 1f, 0.6f),
            colorMultiplier = 3f,
            fadeDuration = 0.25f,
        };

        /// <exception cref="ArgumentOutOfRangeException">Thrown for anything but a single state.</exception>
        private static Color ColorOf(ColorBlock block, SelectableStates state) => state switch
        {
            SelectableStates.Normal => block.normalColor,
            SelectableStates.Highlighted => block.highlightedColor,
            SelectableStates.Pressed => block.pressedColor,
            SelectableStates.Selected => block.selectedColor,
            SelectableStates.Disabled => block.disabledColor,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, "Not a single state.")
        };

        /// <summary>
        /// Asserts that every state the mask excludes came through with the authored colour untouched.
        /// </summary>
        private static void AssertUntouchedOutside(SelectableStates written, ColorBlock result)
        {
            foreach (var state in EveryState)
            {
                if (written.HasFlag(state)) continue;

                Assert.AreEqual(
                    ColorOf(Authored(), state),
                    ColorOf(result, state),
                    $"{state} sits outside the {written} mask and should have passed through untouched.");
            }
        }

        private static void AssertSameColor(Color expected, Color actual, string message)
        {
            Assert.AreEqual(expected.r, actual.r, Tolerance, $"{message}: red.");
            Assert.AreEqual(expected.g, actual.g, Tolerance, $"{message}: green.");
            Assert.AreEqual(expected.b, actual.b, Tolerance, $"{message}: blue.");
            Assert.AreEqual(expected.a, actual.a, Tolerance, $"{message}: alpha.");
        }
    }
}
