using System;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using System.Globalization;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ThousandsSeparatorConverter"/> — the culture's own separator and an
    /// authored override, across the numeric overloads.
    /// </summary>
    [TestFixture]
    public sealed class ThousandsSeparatorConverterTests
    {
        [Test]
        public void Convert_Long_UsesTheCulturesSeparatorByDefault() =>
            Assert.AreEqual("1,234,567", new ThousandsSeparatorConverter(string.Empty, CultureInfoMode.InvariantCulture).Convert(1234567L));

        [Test]
        public void Convert_Int_UsesTheCulturesSeparatorByDefault() =>
            Assert.AreEqual("1,234,567", new ThousandsSeparatorConverter(string.Empty, CultureInfoMode.InvariantCulture).Convert(1234567));

        [TestCase(" ", "1 234 567")]
        [TestCase("_", "1_234_567")]
        [TestCase("'", "1'234'567")]
        // Written escaped because the character a game most often reaches for here is invisible.
        [TestCase("\u2009", "1\u2009234\u2009567")]
        public void Convert_CustomSeparator_ReplacesTheCulturesOwn(string separator, string expected) =>
            Assert.AreEqual(expected, new ThousandsSeparatorConverter(separator, CultureInfoMode.InvariantCulture).Convert(1234567L));

        [Test]
        public void Convert_NegativeValue_KeepsTheSign() =>
            Assert.AreEqual("-1,234", new ThousandsSeparatorConverter(string.Empty, CultureInfoMode.InvariantCulture).Convert(-1234));

        // The grouping is a whole-number rendering, so a fractional input is truncated toward zero
        // rather than carried into the text.
        [Test]
        public void Convert_Double_TruncatesTowardZero() =>
            Assert.AreEqual(
                "1,234,567",
                ((IConverter<double, string>)new ThousandsSeparatorConverter(string.Empty, CultureInfoMode.InvariantCulture))
                    .Convert(1234567.89d));

        [Test]
        public void Convert_Float_TruncatesTowardZero() =>
            Assert.AreEqual(
                "1,234",
                ((IConverter<float, string>)new ThousandsSeparatorConverter(string.Empty, CultureInfoMode.InvariantCulture))
                    .Convert(1234.9f));

        // long.MinValue has no positive counterpart of its own width, so it is negated as a double.
        [Test]
        public void Convert_LongMinValue_Formats() =>
            Assert.AreEqual(
                "-9,223,372,036,854,775,808",
                new ThousandsSeparatorConverter(string.Empty, CultureInfoMode.InvariantCulture).Convert(long.MinValue));

        [Test]
        public void Convert_IntMinValue_Formats() =>
            Assert.AreEqual(
                "-2,147,483,648",
                new ThousandsSeparatorConverter(string.Empty, CultureInfoMode.InvariantCulture).Convert(int.MinValue));

        // The separator field ships empty, and empty is the one value that cannot mean "no
        // separator": it means "whatever the device is set to".
        [Test]
        [SetCulture("")]
        public void Convert_EmptySeparator_FollowsTheDeviceCulture() =>
            Assert.AreEqual("1,234,567", new ThousandsSeparatorConverter().Convert(1234567L));

        [Test]
        [SetCulture("de-DE")]
        public void Convert_EmptySeparator_FollowsTheDeviceCulture_German() =>
            Assert.AreEqual("1.234.567", new ThousandsSeparatorConverter().Convert(1234567L));

        // An authored separator replaces the culture's separator but not its grouping, and it does so
        // whichever culture the device happens to be set to.
        [Test]
        [SetCulture("de-DE")]
        public void Convert_AuthoredSeparator_SurvivesTheDeviceCulture() =>
            Assert.AreEqual("1_234_567", new ThousandsSeparatorConverter("_").Convert(1234567L));

        // An authored separator replaces the culture's separator but not its grouping. India groups
        // the last three digits and then in pairs, so the authored character lands in Indian places.
        [Test]
        public void Convert_AuthoredSeparator_KeepsTheCultureGrouping()
        {
            CultureInfo india;

            try
            {
                india = CultureInfo.GetCultureInfo("en-IN");
            }
            catch (CultureNotFoundException)
            {
                Assert.Ignore("en-IN is not present in this runtime's culture data.");
                return;
            }

            Assume.That(india.NumberFormat.NumberGroupSizes, Is.EqualTo(new[] { 3, 2 }));

            var previous = System.Threading.Thread.CurrentThread.CurrentCulture;

            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = india;
                Assert.AreEqual("12_34_567", new ThousandsSeparatorConverter("_").Convert(1234567L));
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = previous;
            }
        }

        // A NumberFormatInfo taken from a culture is shared process-wide and read-only. Setting the
        // separator on it rather than on a clone would either throw here or silently re-format every
        // number the rest of the game prints.
        [Test]
        public void Convert_AuthoredSeparator_LeavesTheSharedCultureFormatAlone()
        {
            new ThousandsSeparatorConverter("_", CultureInfoMode.InvariantCulture).Convert(1234567L);

            Assert.AreEqual(",", CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator);
        }

        // The clone is cached because a binder pushes on every notification rather than on every
        // change. Re-authoring the separator has to invalidate that cache; a stale clone would keep
        // writing the old separator for the rest of the session.
        [Test]
        public void Convert_SeparatorReauthored_RebuildsTheCachedFormat()
        {
            var converter = new ThousandsSeparatorConverter("_", CultureInfoMode.InvariantCulture);
            Assert.AreEqual("1_234_567", converter.Convert(1234567L));

            SetSeparator(converter, "'");

            Assert.AreEqual("1'234'567", converter.Convert(1234567L));
        }

        // The same cache, from the other side: repeated pushes must not drift.
        [Test]
        public void Convert_RepeatedCalls_KeepFormattingTheSameWay()
        {
            var converter = new ThousandsSeparatorConverter("_", CultureInfoMode.InvariantCulture);

            Assert.AreEqual("1_234_567", converter.Convert(1234567L));
            Assert.AreEqual("7_654_321", converter.Convert(7654321L));
            Assert.AreEqual("1_234_567", converter.Convert(1234567L));
        }

        // The separator has no setter on a live instance, so the test writes it the way the
        // Inspector does.
        private static void SetSeparator(ThousandsSeparatorConverter converter, string separator)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var field = typeof(ThousandsSeparatorConverter).GetField("_separator", flags);
            if (field is null) throw new InvalidOperationException("ThousandsSeparatorConverter has no _separator field.");

            field.SetValue(converter, separator);

            // Unity reads the object again after an Inspector edit, which is where a converter
            // holding a cache built from its settings drops it.
            if (converter is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();
        }
    }
}
