using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="ThousandsSeparatorConverter"/> — the culture's own separator and an
    /// authored override, across the numeric overloads.
    /// </summary>
    [TestFixture]
    internal sealed class ThousandsSeparatorConverterTests
    {
        [Test]
        public void Convert_Long_UsesTheCulturesSeparatorByDefault() =>
            Assert.AreEqual("1,234,567", new ThousandsSeparatorConverter(string.Empty, CultureInfoMode.InvariantCulture).Convert(1234567L));

        [Test]
        public void Convert_Int_UsesTheCulturesSeparatorByDefault() =>
            Assert.AreEqual("1,234,567", new ThousandsSeparatorConverter(string.Empty, CultureInfoMode.InvariantCulture).Convert(1234567));

        [Test]
        public void Convert_CustomSeparator_ReplacesTheCulturesOwn() =>
            Assert.AreEqual("1 234 567", new ThousandsSeparatorConverter(" ", CultureInfoMode.InvariantCulture).Convert(1234567L));

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
    }
}
