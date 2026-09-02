using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CurrencyConverter"/> — the symbol position, decimals, digit grouping,
    /// and the sign placed in front of the symbol.
    /// </summary>
    [TestFixture]
    public sealed class CurrencyConverterTests
    {
        [Test]
        public void Convert_SymbolBefore_LeadsTheAmount() =>
            Assert.AreEqual("$5", new CurrencyConverter("$").Convert(5d));

        [Test]
        public void Convert_SymbolAfter_TrailsTheAmount() =>
            Assert.AreEqual("5€", new CurrencyConverter("€", SymbolPosition.After).Convert(5d));

        [Test]
        public void Convert_Decimals_AreShown() =>
            Assert.AreEqual("$5.50", new CurrencyConverter("$", decimals: 2).Convert(5.5d));

        // A debt reads as -$5, not $-5: the sign is placed outside the symbol.
        [Test]
        public void Convert_NegativeAmount_KeepsTheSignInFrontOfTheSymbol() =>
            Assert.AreEqual("-$5", new CurrencyConverter("$").Convert(-5d));

        // The sign still leads even with the symbol trailing the amount: -5€, not 5-€ or 5€-.
        [Test]
        public void Convert_NegativeAmount_SymbolAfter_KeepsTheSignInFront() =>
            Assert.AreEqual("-5€", new CurrencyConverter("€", SymbolPosition.After).Convert(-5d));

        [Test]
        public void Convert_GroupsDigitsByDefault() =>
            Assert.AreEqual("$1,234", new CurrencyConverter("$").Convert(1234d));
    }
}
