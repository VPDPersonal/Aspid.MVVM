using System;
using UnityEngine;
using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// A binder must never run the forward converter on a value going back to the ViewModel.
    /// </summary>
    /// <remarks>
    /// The base <see cref="TargetBinder{TTarget, TProperty, TConverter}"/> was fixed to route the
    /// reverse direction through <see cref="ITwoWayConverter{TFrom, TTo}"/>, but four binders carry
    /// their own private converter field and never inherited that: <c>GameObjectTagBinder</c>,
    /// <c>GameObjectTagMonoBinder</c>, <c>ObjectNameBinder</c> and <c>ObjectNameMonoBinder</c> each
    /// pushed <c>GetConvertedValue(target)</c> to the ViewModel on bind.
    /// <para>
    /// The bug hides behind the converters people test with. Inversion and negation are their own
    /// inverse, so applying the forward conversion twice looks correct; a converter that appends a
    /// suffix shows it immediately. That is why the converter here is deliberately not an involution.
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
                    "a one-way converter cannot be undone, so the raw value is the only honest answer — "
                    + "and it must not be the forward-converted one");
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
