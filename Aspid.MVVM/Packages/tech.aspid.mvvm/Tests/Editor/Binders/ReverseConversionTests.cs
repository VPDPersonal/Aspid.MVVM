using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// A binder must never run the forward converter on a value going back to the ViewModel.
    /// </summary>
    /// <remarks>
    /// Four binders carry their own private converter field and never inherited the base fix that routes
    /// the reverse direction through <see cref="ITwoWayConverter{TFrom, TTo}"/>.
    /// <para>
    /// The bug hides behind the converters people test with: inversion is its own inverse, so applying
    /// the forward conversion twice looks correct. The converter here is deliberately not an involution.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ReverseConversionTests
    {
        [Test]
        public void GameObjectTagBinder_OneWayToSource_SendsTheValueBackUndone()
        {
            var go = new GameObject("probe") { tag = "Player" };
            try
            {
                string? received = null;
                var member = new OneWayToSourceBindableMember<string>(value => received = value);
                var binder = new GameObjectTagBinder(go, new SuffixConverter(), BindMode.OneWayToSource);

                binder.Bind(member);

                Assert.AreEqual("Player", received, "the tag should arrive as the ViewModel would hold it");
            }
            finally { UnityEngine.Object.DestroyImmediate(go); }
        }

        [Test]
        public void GameObjectTagBinder_OneWayToSource_WithAOneWayConverter_SendsTheRawValue()
        {
            var go = new GameObject("probe") { tag = "Player" };
            try
            {
                string? received = null;
                var member = new OneWayToSourceBindableMember<string>(value => received = value);
                var binder = new GameObjectTagBinder(go, new OneWaySuffixConverter(), BindMode.OneWayToSource);

                binder.Bind(member);

                Assert.AreEqual(
                    "Player",
                    received,
                    "a one-way converter cannot be undone, so the raw value is the only honest answer — " +
                    "and it must not be the forward-converted one");
            }
            finally { UnityEngine.Object.DestroyImmediate(go); }
        }

        [Test]
        public void ObjectNameBinder_OneWayToSource_SendsTheValueBackUndone()
        {
            var go = new GameObject("Hero");
            try
            {
                string? received = null;
                var member = new OneWayToSourceBindableMember<string>(value => received = value);
                var binder = new ObjectNameBinder(go, new SuffixConverter(), BindMode.OneWayToSource);

                binder.Bind(member);

                Assert.AreEqual("Hero", received);
            }
            finally { UnityEngine.Object.DestroyImmediate(go); }
        }

        [Test]
        public void TargetFloatBinder_OneWayToSource_SendsTheValueBackUndone()
        {
            float? received = null;
            var member = new OneWayToSourceBindableMember<float>(value => received = value);
            var binder = new ProbeFloatBinder(new FloatProbe { Value = 250f }, new ScaleConverter(), BindMode.OneWayToSource);

            binder.Bind(member);

            Assert.IsNotNull(received, "the typed channel a float member binds through must be raised");
            Assert.AreEqual(2.5f, received!.Value, 1e-4f, "the ViewModel expects its own scale, not the View's");
        }

        // The property's own type reaches the base ValueChanged event, every other numeric type reaches
        // the INumberReverseBinder channel. Both have to carry the converted-back value.
        [Test]
        public void TargetFloatBinder_OneWayToSource_SendsTheValueBackUndoneOnTheNumericChannel()
        {
            int? received = null;
            var member = new OneWayToSourceBindableMember<int>(value => received = value);
            var binder = new ProbeFloatBinder(new FloatProbe { Value = 250f }, new ScaleConverter(), BindMode.OneWayToSource);

            binder.Bind(member);

            Assert.AreEqual(2, received, "250 undone by a x100 converter is 2.5, truncated to 2");
        }

        [Test]
        public void TargetFloatBinder_OneWayToSource_WithAOneWayConverter_SendsTheRawValueAndWarns()
        {
            LogAssert.Expect(LogType.Warning, new Regex("converts one way only"));

            float? received = null;
            var member = new OneWayToSourceBindableMember<float>(value => received = value);
            var binder = new ProbeFloatBinder(new FloatProbe { Value = 250f }, new OneWayScaleConverter(), BindMode.OneWayToSource);

            binder.Bind(member);

            Assert.AreEqual(250f, received!.Value, 1e-4f, "a one-way converter cannot be undone, so the raw value is the only honest answer");
        }

        [Test]
        public void OneWayToSourceValue_SendsTheInitialValueBackUndone()
        {
            float? received = null;
            var member = new OneWayToSourceBindableMember<float>(value => received = value);
            var binder = new OneWayToSourceValue<float>(250f, new ScaleConverter());

            binder.Bind(member);

            Assert.IsNotNull(received);
            Assert.AreEqual(2.5f, received!.Value, 1e-4f, "the initial push must land in the same space as every later one");
        }

        private sealed class FloatProbe
        {
            public float Value { get; set; }
        }

        private sealed class ProbeFloatBinder : TargetFloatBinder<FloatProbe>
        {
            public ProbeFloatBinder(FloatProbe target, IConverter<float, float>? converter, BindMode mode)
                : base(target, converter, mode) { }

            protected override float Property
            {
                get => Target.Value;
                set => Target.Value = value;
            }
        }

        // Scaling is not its own inverse either, so the doubled forward pass shows up as 25000.
        private sealed class ScaleConverter : ITwoWayConverter<float, float>
        {
            public float Convert(float value) => value * 100f;

            public float ConvertBack(float value) => value / 100f;
        }

        private sealed class OneWayScaleConverter : IConverter<float, float>
        {
            public float Convert(float value) => value * 100f;
        }

        // Appending is not its own inverse, so a doubled forward pass is visible. An inverting or
        // negating converter would let the bug through.
        private sealed class SuffixConverter : ITwoWayConverter<string?, string?>
        {
            public string? Convert(string? value) => value + "!";

            public string? ConvertBack(string? value) =>
                value is not null && value.EndsWith("!", StringComparison.Ordinal)
                    ? value[..^1]
                    : value;
        }

        private sealed class OneWaySuffixConverter : IConverter<string?, string?>
        {
            public string? Convert(string? value) => value + "!";
        }
    }
}
