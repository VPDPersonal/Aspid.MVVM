using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="EasingConverter"/> and the thirty-one <see cref="EaseType"/> curves it
    /// dispatches to — the two endpoints, the seams between branches, the range each family actually
    /// occupies, and the <c>clamp</c> flag.
    /// </summary>
    /// <remarks>
    /// The <c>InOut</c> curves splice two formulas at <c>t = 0.5</c>, which never shows at <c>t = 0</c> or <c>t = 1</c>, so every curve is scanned end to end
    /// and pinned at a midpoint. Two documented claims do not survive executing the formulas; the tests naming them say so.
    /// </remarks>
    [TestFixture]
    public sealed class EasingConverterTests
    {
        // Fine enough that CircIn's vertical tangent at t = 1 — the steepest legitimate slope in the
        // set — still only moves 0.0045 between neighbouring samples, which leaves room to call
        // anything larger a broken seam.
        private const int Samples = 100_000;

        // Back and Elastic are the two families the enum documents as leaving 0..1, and that claim is
        // the one Evaluate_BackAndElastic_LeaveTheUnitRange measures. Everything else — Bounce very
        // much included — is required to stay inside.
        private static readonly EaseType[] _overshooting =
        {
            EaseType.BackIn, EaseType.BackOut, EaseType.BackInOut,
            EaseType.ElasticIn, EaseType.ElasticOut, EaseType.ElasticInOut,
        };

        // The three families that reverse direction somewhere. Bounce belongs here even though it
        // never leaves 0..1: staying in range and moving in one direction are different properties,
        // and the enum's "shaped" wording blurs them.
        private static readonly EaseType[] _reversing =
        {
            EaseType.BackIn, EaseType.BackOut, EaseType.BackInOut,
            EaseType.ElasticIn, EaseType.ElasticOut, EaseType.ElasticInOut,
            EaseType.BounceIn, EaseType.BounceOut, EaseType.BounceInOut,
        };

        // ---------------------------------------------------------------- endpoints

        // A curve that misses 1 leaves a health bar a hair short of full forever, and one that misses
        // 0 leaves a faded-out element faintly visible. Iterating the enum rather than listing the
        // curves also means a new EaseType added without a switch arm fails here, on the unexpected
        // error the default branch reports, instead of shipping.
        [Test]
        public void Evaluate_EveryCurve_PinsBothEndpoints()
        {
            foreach (EaseType ease in Enum.GetValues(typeof(EaseType)))
            {
                Assert.AreEqual(0f, EasingConverter.Evaluate(ease, 0f), 1e-6f, $"{ease} at t = 0");
                Assert.AreEqual(1f, EasingConverter.Evaluate(ease, 1f), 1e-6f, $"{ease} at t = 1");
            }
        }

        // ---------------------------------------------------------------- shape of each formula

        // One value per In and Out curve. These are the numbers a wrong exponent, a dropped minus or
        // a swapped In/Out body moves first — QuartIn and QuintIn differ only here, not at the ends.
        // The signed rows are the point of the exercise: BackIn and ElasticIn are already below zero
        // halfway through, and BackOut is already above one.
        [TestCase(EaseType.Linear, 0.5f)]
        [TestCase(EaseType.SineIn, 0.29289323f)]
        [TestCase(EaseType.SineOut, 0.70710677f)]
        [TestCase(EaseType.QuadIn, 0.25f)]
        [TestCase(EaseType.QuadOut, 0.75f)]
        [TestCase(EaseType.CubicIn, 0.125f)]
        [TestCase(EaseType.CubicOut, 0.875f)]
        [TestCase(EaseType.QuartIn, 0.0625f)]
        [TestCase(EaseType.QuartOut, 0.9375f)]
        [TestCase(EaseType.QuintIn, 0.03125f)]
        [TestCase(EaseType.QuintOut, 0.96875f)]
        [TestCase(EaseType.ExpoIn, 0.03125f)]
        [TestCase(EaseType.ExpoOut, 0.96875f)]
        [TestCase(EaseType.CircIn, 0.13397461f)]
        [TestCase(EaseType.CircOut, 0.86602539f)]
        [TestCase(EaseType.BackIn, -0.08769751f)]
        [TestCase(EaseType.BackOut, 1.08769751f)]
        [TestCase(EaseType.ElasticIn, -0.01562499f)]
        [TestCase(EaseType.ElasticOut, 1.015625f)]
        [TestCase(EaseType.BounceIn, 0.234375f)]
        [TestCase(EaseType.BounceOut, 0.765625f)]
        public void Evaluate_AtTheMidpoint_MatchesTheMeasuredValue(EaseType ease, float expected) =>
            Assert.AreEqual(expected, EasingConverter.Evaluate(ease, 0.5f), 1e-6f);

        // Each InOut curve is two half-formulas joined at t = 0.5, and both halves are supposed to
        // arrive at 0.5 there. This is the cheapest possible check that the branch split and the
        // remapping of t into each half agree.
        [Test]
        public void Evaluate_EveryInOutCurve_PassesThroughTheMidpointExactly()
        {
            foreach (EaseType ease in Enum.GetValues(typeof(EaseType)))
            {
                if (!ease.ToString().EndsWith("InOut", StringComparison.Ordinal)) continue;

                Assert.AreEqual(0.5f, EasingConverter.Evaluate(ease, 0.5f), 1e-6f, $"{ease} at t = 0.5");
            }
        }

        // An InOut curve is meant to be point-symmetric about (0.5, 0.5). If only the second branch
        // gets a corrected constant the ends still land on 0 and 1 and the midpoint still reads 0.5 —
        // this is what notices.
        [Test]
        public void Evaluate_EveryInOutCurve_IsSymmetricAboutTheMidpoint()
        {
            foreach (EaseType ease in Enum.GetValues(typeof(EaseType)))
            {
                if (!ease.ToString().EndsWith("InOut", StringComparison.Ordinal)) continue;

                for (var i = 0; i <= 1000; i++)
                {
                    var t = (float)(i / 1000d);
                    var sum = EasingConverter.Evaluate(ease, t) + EasingConverter.Evaluate(ease, 1f - t);

                    Assert.AreEqual(1f, sum, 1e-5f, $"{ease}: f({t}) + f({1f - t})");
                }
            }
        }

        // In and Out are the same shape read in opposite directions, so In(t) + Out(1 - t) is 1 for
        // every family. Bounce is deliberately absent: BounceIn is literally defined as
        // 1 - BounceOut(1 - t), so the identity holds by construction and the row would assert
        // nothing about the formula.
        [TestCase(EaseType.SineIn, EaseType.SineOut)]
        [TestCase(EaseType.QuadIn, EaseType.QuadOut)]
        [TestCase(EaseType.CubicIn, EaseType.CubicOut)]
        [TestCase(EaseType.QuartIn, EaseType.QuartOut)]
        [TestCase(EaseType.QuintIn, EaseType.QuintOut)]
        [TestCase(EaseType.ExpoIn, EaseType.ExpoOut)]
        [TestCase(EaseType.CircIn, EaseType.CircOut)]
        [TestCase(EaseType.BackIn, EaseType.BackOut)]
        [TestCase(EaseType.ElasticIn, EaseType.ElasticOut)]
        public void Evaluate_InAndOut_AreTheSameShapeReadBackwards(EaseType easeIn, EaseType easeOut)
        {
            for (var i = 0; i <= 1000; i++)
            {
                var t = (float)(i / 1000d);
                var sum = EasingConverter.Evaluate(easeIn, t) + EasingConverter.Evaluate(easeOut, 1f - t);

                Assert.AreEqual(1f, sum, 1e-5f, $"{easeIn}({t}) + {easeOut}({1f - t})");
            }
        }

        // ---------------------------------------------------------------- continuity

        // The real target is a branch that does not meet its neighbour: BounceOut's four parabolas,
        // the t < 0.5 split in every InOut curve, and BackInOut's two halves. A seam bug moves one
        // branch by a visible amount, so a bound twice the largest legitimate step still catches it.
        // Note this deliberately passes for the Expo family, whose 1/1024 step is real and is pinned
        // separately below.
        [Test]
        public void Evaluate_EveryCurve_HasNoStepBetweenItsBranches()
        {
            foreach (EaseType ease in Enum.GetValues(typeof(EaseType)))
            {
                var worst = WorstStep(ease, out var at);

                Assert.That(worst, Is.LessThan(0.01f), $"{ease} steps by {worst} at t = {at}");
            }
        }

        // Penner's Expo is 2^(10t - 10), which is 1/1024 at t = 0 rather than 0, so the guard that
        // forces the endpoint leaves a genuine jump behind it: this family is NOT continuous, whatever
        // "starting almost flat" in the enum docs suggests. Anything driven by ExpoIn snaps to 0.1% on
        // its first frame instead of leaving the origin smoothly.
        [TestCase(EaseType.ExpoIn, 0.0009765625f)]
        [TestCase(EaseType.ExpoInOut, 0.00048828125f)]
        public void Evaluate_ExpoFamily_StepsOffZeroInsteadOfLeavingIt(EaseType ease, float justAfterZero)
        {
            Assert.AreEqual(0f, EasingConverter.Evaluate(ease, 0f), 1e-7f, "the forced endpoint");
            Assert.AreEqual(justAfterZero, EasingConverter.Evaluate(ease, 1e-9f), 1e-7f, "the formula beside it");
        }

        // The same discontinuity at the other end, where it matters more: the curve arrives at
        // 0.99902 and is then snapped to 1, so the last frame of the animation is a visible jump.
        [TestCase(EaseType.ExpoOut, 0.99902278f)]
        [TestCase(EaseType.ExpoInOut, 0.99951106f)]
        public void Evaluate_ExpoFamily_StepsOntoOneInsteadOfReachingIt(EaseType ease, float justBeforeOne)
        {
            Assert.AreEqual(justBeforeOne, EasingConverter.Evaluate(ease, 0.9999f), 1e-6f, "the formula short of the end");
            Assert.AreEqual(1f, EasingConverter.Evaluate(ease, 1f), 1e-7f, "the forced endpoint");
        }

        // ---------------------------------------------------------------- range

        // The measured extremes, not the documented ones. Back reaches a tenth past each end and
        // Elastic more than a third past — the numbers a binder author needs in order to decide
        // whether a ClampNumberConverter has to follow this one.
        [TestCase(EaseType.BackIn, -0.10000409f, 1f)]
        [TestCase(EaseType.BackOut, 0f, 1.10000408f)]
        [TestCase(EaseType.BackInOut, -0.10015138f, 1.10015142f)]
        [TestCase(EaseType.ElasticIn, -0.37309808f, 1f)]
        [TestCase(EaseType.ElasticOut, 0f, 1.37309813f)]
        [TestCase(EaseType.ElasticInOut, -0.11834795f, 1.11834800f)]
        public void Evaluate_BackAndElastic_LeaveTheUnitRange(EaseType ease, float expectedMin, float expectedMax)
        {
            Range(ease, out var min, out var max);

            Assert.AreEqual(expectedMin, min, 1e-3f, $"{ease} minimum");
            Assert.AreEqual(expectedMax, max, 1e-3f, $"{ease} maximum");
        }

        // The claim being checked is the enum's "only Back and Elastic leave the 0..1 range; Bounce
        // stays inside it", and it holds — measured, Bounce bottoms out at exactly 0 and tops out at
        // exactly 1. Which is why a Bounce curve is safe on Image.fillAmount and a Back curve is not.
        [Test]
        public void Evaluate_EveryCurveOutsideBackAndElastic_StaysInsideTheUnitRange()
        {
            foreach (EaseType ease in Enum.GetValues(typeof(EaseType)))
            {
                if (Array.IndexOf(_overshooting, ease) >= 0) continue;

                Range(ease, out var min, out var max);

                Assert.That(min, Is.GreaterThanOrEqualTo(-1e-6f), $"{ease} dips to {min}");
                Assert.That(max, Is.LessThanOrEqualTo(1f + 1e-6f), $"{ease} rises to {max}");
            }
        }

        // ---------------------------------------------------------------- direction

        // Everything up to Circ is a one-way ramp; if one of them ever went backwards a progress bar
        // would visibly retreat mid-fill. The tolerance is slack for float noise only — measured, not
        // one of these curves descends by a single ulp across a hundred thousand samples.
        [Test]
        public void Evaluate_EveryCurveOutsideTheShapedFamilies_NeverGoesBackwards()
        {
            foreach (EaseType ease in Enum.GetValues(typeof(EaseType)))
            {
                if (Array.IndexOf(_reversing, ease) >= 0) continue;

                var descent = WorstDescent(ease);

                Assert.That(descent, Is.LessThanOrEqualTo(1e-6f), $"{ease} falls back by {descent}");
            }
        }

        // Bounce reverses without ever overshooting, which is the distinction the enum docs elide by
        // grouping it with Back and Elastic as "shaped". It touches the ceiling three times on the way
        // in and drops away from it each time; the seams between the four parabolas are exactly where
        // it touches, so this doubles as the seam check for BounceOut.
        [TestCase(0.36363637f)]
        [TestCase(0.72727275f)]
        [TestCase(0.90909094f)]
        public void Evaluate_BounceOut_TouchesOneAtEverySeam(float seam) =>
            Assert.AreEqual(1f, EasingConverter.Evaluate(EaseType.BounceOut, seam), 1e-6f);

        // The three rests between those touches, each a fixed fraction of the drop before it. Exact
        // constants, so a mistyped 0.9375 or 0.984375 has nowhere to hide.
        [TestCase(0.54545456f, 0.75f)]
        [TestCase(0.81818181f, 0.9375f)]
        [TestCase(0.95454544f, 0.984375f)]
        public void Evaluate_BounceOut_SettlesToAShrinkingTrough(float trough, float expected) =>
            Assert.AreEqual(expected, EasingConverter.Evaluate(EaseType.BounceOut, trough), 1e-6f);

        // The descent itself, stated as an inequality so it cannot be satisfied by a flat curve.
        [Test]
        public void Evaluate_BounceOut_FallsAwayFromTheCeilingAfterTheFirstTouch()
        {
            var atSeam = EasingConverter.Evaluate(EaseType.BounceOut, 0.36363637f);
            var atTrough = EasingConverter.Evaluate(EaseType.BounceOut, 0.54545456f);

            Assert.That(atTrough, Is.LessThan(atSeam - 0.2f), "the first bounce barely drops");
        }

        // ---------------------------------------------------------------- documented pull order

        // The enum's remarks order the families "by how hard they pull — Sine, Quad, Cubic, Quart,
        // Quint, Expo, Circ", which reads as Circ being the hardest. Measured at the midpoint it is
        // not: CircIn sits above CubicIn, i.e. gentler than the third-softest polynomial, and ExpoIn
        // does not beat QuintIn either — the two are bit-identical there. The behavior is correct
        // Penner; the sentence describing it is wrong, so this test pins the behavior.
        [Test]
        public void Evaluate_CircAndExpo_BreakTheDocumentedPullOrder()
        {
            Assert.That(
                EasingConverter.Evaluate(EaseType.CircIn, 0.5f),
                Is.GreaterThan(EasingConverter.Evaluate(EaseType.CubicIn, 0.5f)),
                "CircIn is documented as the hardest pull but is gentler than CubicIn at the midpoint");

            Assert.AreEqual(
                EasingConverter.Evaluate(EaseType.QuintIn, 0.5f),
                EasingConverter.Evaluate(EaseType.ExpoIn, 0.5f),
                0f,
                "ExpoIn is documented as pulling harder than QuintIn but ties it exactly at the midpoint");
        }

        // ---------------------------------------------------------------- the clamp flag

        // With clamping on, anything outside 0..1 collapses onto an endpoint. A binder fed a health
        // value that briefly exceeds its maximum must not produce a curve value from the extrapolated
        // formula — for BackOut that would be 5.4, and for ExpoIn 1024.
        [Test]
        public void Convert_Clamped_FoldsEveryOutOfRangeInputOntoAnEndpoint()
        {
            foreach (EaseType ease in Enum.GetValues(typeof(EaseType)))
            {
                var converter = new EasingConverter(ease);

                Assert.AreEqual(0f, converter.Convert(-1f), 1e-6f, $"{ease} below the range");
                Assert.AreEqual(0f, converter.Convert(-0.001f), 1e-6f, $"{ease} just below the range");
                Assert.AreEqual(1f, converter.Convert(2f), 1e-6f, $"{ease} above the range");
                Assert.AreEqual(1f, converter.Convert(1.001f), 1e-6f, $"{ease} just above the range");
            }
        }

        // With clamping off the formula is evaluated as written, and the polynomials run away fast.
        // QuadIn is the interesting row: an even power folds a negative input back up to +1, so
        // turning clamping off does not merely extend the curve downwards.
        [TestCase(EaseType.Linear, 2f, 2f)]
        [TestCase(EaseType.Linear, -1f, -1f)]
        [TestCase(EaseType.QuadIn, -1f, 1f)]
        [TestCase(EaseType.CubicIn, 2f, 8f)]
        [TestCase(EaseType.ExpoIn, 2f, 1024f)]
        [TestCase(EaseType.BackOut, 2f, 5.4031601f)]
        public void Convert_Unclamped_EvaluatesTheFormulaAsWritten(EaseType ease, float value, float expected) =>
            Assert.AreEqual(expected, new EasingConverter(ease, clamp: false).Convert(value), 1e-4f);

        // The documented split: the flag guards the input only. Clamping is on here and the result
        // still leaves 0..1, because that overshoot is the whole point of Back — which is exactly why
        // a target with a hard range of its own needs a ClampNumberConverter after this converter.
        [Test]
        public void Convert_Clamped_StillLetsTheOutputLeaveTheUnitRange()
        {
            Assert.That(new EasingConverter(EaseType.BackOut).Convert(0.5f), Is.GreaterThan(1f));
            Assert.That(new EasingConverter(EaseType.BackIn).Convert(0.5f), Is.LessThan(0f));
        }

        // Circ takes a square root of 1 - t², which goes negative the moment t escapes 0..1. Without
        // the SafeSqrt guard this returns NaN, and a NaN reaching a Transform corrupts it silently
        // rather than throwing.
        [TestCase(EaseType.CircIn, -1f, 1f)]
        [TestCase(EaseType.CircIn, 2f, 1f)]
        [TestCase(EaseType.CircOut, -1f, 0f)]
        [TestCase(EaseType.CircOut, 2f, 0f)]
        [TestCase(EaseType.CircInOut, -1f, 0.5f)]
        [TestCase(EaseType.CircInOut, 2f, 0.5f)]
        public void Convert_Unclamped_CircClampsItsRadicandInsteadOfReturningNaN(EaseType ease, float value, float expected) =>
            Assert.AreEqual(expected, new EasingConverter(ease, clamp: false).Convert(value), 1e-6f);

        // The same guarantee for the whole set, since any curve can be picked in the Inspector and
        // clamping can be switched off next to it.
        [Test]
        public void Convert_Unclamped_NeverReturnsNaNOrInfinity()
        {
            foreach (EaseType ease in Enum.GetValues(typeof(EaseType)))
            {
                var converter = new EasingConverter(ease, clamp: false);

                foreach (var value in new[] { -1f, -0.25f, 1.25f, 2f })
                {
                    var result = converter.Convert(value);

                    Assert.IsFalse(float.IsNaN(result), $"{ease} returned NaN at {value}");
                    Assert.IsFalse(float.IsInfinity(result), $"{ease} returned infinity at {value}");
                }
            }
        }

        // The parameterless constructor is what a freshly added Inspector row uses, and it is not a
        // pass-through: it eases out quadratically and clamps.
        [Test]
        public void Convert_DefaultConstructed_EasesOutQuadraticallyAndClamps()
        {
            var converter = new EasingConverter();

            Assert.AreEqual(0.75f, converter.Convert(0.5f), 1e-6f, "the QuadOut midpoint");
            Assert.AreEqual(1f, converter.Convert(2f), 1e-6f, "clamped above");
            Assert.AreEqual(0f, converter.Convert(-1f), 1e-6f, "clamped below");
        }

        // ---------------------------------------------------------------- undeclared curve

        // A serialized enum survives its declaration being reordered or an entry being deleted, so a
        // prefab can hand this converter a value no branch matches. It has to report that on every
        // push rather than silently returning 0.
        [Test]
        public void Evaluate_UndeclaredCurve_ReportsAndReturnsThePositionUnchanged()
        {
            ExpectUndeclaredCurve();

            Assert.AreEqual(0.5f, EasingConverter.Evaluate((EaseType)999, 0.5f), 1e-6f);
        }

        [Test]
        public void Convert_UndeclaredCurve_ReportsAndReturnsTheValueUnchanged()
        {
            ExpectUndeclaredCurve();

            Assert.AreEqual(0.5f, new EasingConverter((EaseType)999).Convert(0.5f), 1e-6f);
        }

        private static void ExpectUndeclaredCurve() =>
            LogAssert.Expect(LogType.Error, new Regex("EasingConverter.*not a declared EaseType"));

        // ---------------------------------------------------------------- helpers

        private static float WorstStep(EaseType ease, out float at)
        {
            var worst = 0f;
            at = 0f;

            var previous = EasingConverter.Evaluate(ease, 0f);

            for (var i = 1; i <= Samples; i++)
            {
                var t = (float)((double)i / Samples);
                var current = EasingConverter.Evaluate(ease, t);
                var step = Mathf.Abs(current - previous);

                if (step > worst)
                {
                    worst = step;
                    at = t;
                }

                previous = current;
            }

            return worst;
        }

        private static float WorstDescent(EaseType ease)
        {
            var worst = 0f;
            var previous = EasingConverter.Evaluate(ease, 0f);

            for (var i = 1; i <= Samples; i++)
            {
                var current = EasingConverter.Evaluate(ease, (float)((double)i / Samples));
                var descent = previous - current;

                if (descent > worst) worst = descent;

                previous = current;
            }

            return worst;
        }

        private static void Range(EaseType ease, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;

            for (var i = 0; i <= Samples; i++)
            {
                var value = EasingConverter.Evaluate(ease, (float)((double)i / Samples));

                if (value < min) min = value;
                if (value > max) max = value;
            }
        }
        // The curves are Unity's float math; the double width carries a float's precision through them.
        [Test]
        public void Easing_Double_RunsTheSameCurveAsTheFloatWidth() =>
            Assert.AreEqual(
                new EasingConverter(EaseType.QuadOut).Convert(0.25f),
                ((IConverter<double, double>)new EasingConverter(EaseType.QuadOut)).Convert(0.25d),
                1e-6d);

    }
}
