using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="AnimationCurveConverter"/> — evaluating the curve, the no-curve
    /// guard, and the empty-input-range guard.
    /// </summary>
    [TestFixture]
    public sealed class AnimationCurveConverterTests
    {
        [Test]
        public void AnimationCurve_EvaluatesTheCurve() =>
            Assert.AreEqual(0.5f, new AnimationCurveConverter(AnimationCurve.Linear(0f, 0f, 1f, 1f)).Convert(0.5f), delta: 1e-5f);

        // The curve is the whole of what this converter does, so a keyless one is a broken converter
        // rather than a neutral setting: it passes the value through and says so every time.
        [Test]
        public void AnimationCurve_WithoutACurvePassesThrough()
        {
            LogAssert.Expect(LogType.Error, new Regex("AnimationCurveConverter.*no curve is assigned"));

            Assert.AreEqual(0.37f, new AnimationCurveConverter(new AnimationCurve()).Convert(0.37f), delta: 1e-6f);
        }

        // The input range is read only while the value is normalized, and an empty one has no
        // position to map to; reading the curve at its start keeps the result on the curve instead of
        // the division it replaces handing a NaN to whatever the curve drives.
        [Test]
        public void AnimationCurve_EmptyInputRange_ReportsAndReadsTheCurveAtItsStart()
        {
            var converter = new AnimationCurveConverter(AnimationCurve.Linear(0f, 0f, 1f, 1f));
            SetField(converter, "_normalizeInput", true);
            SetField(converter, "_inputMin", 5f);
            SetField(converter, "_inputMax", 5f);

            LogAssert.Expect(LogType.Error, new Regex("AnimationCurveConverter.*input range is empty"));

            Assert.AreEqual(0f, converter.Convert(7f), delta: 1e-6f);
        }

        // AnimationCurve evaluates in float, so the double width narrows on the way in and carries a
        // float's precision back out.
        [Test]
        public void AnimationCurve_Double_EvaluatesTheCurve() =>
            Assert.AreEqual(
                0.5d,
                ((IConverter<double, double>)new AnimationCurveConverter(AnimationCurve.Linear(0f, 0f, 1f, 1f)))
                    .Convert(0.5d),
                1e-5d);

        // The curve converter's normalization settings are serialized only, so a test reaches them
        // the way the Inspector does.
        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field.SetValue(target, value);
        }
    }
}
