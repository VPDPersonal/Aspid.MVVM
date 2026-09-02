using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="BoolToValueConverter{T}"/> — the two-branch pick, the reverse read,
    /// and the branches-authored-alike and unmatched-value failure modes.
    /// </summary>
    [TestFixture]
    public sealed class BoolToValueConverterTests
    {
        [Test]
        public void Convert_PicksTheAuthoredBranch()
        {
            var converter = new BoolToValueConverter<Color>(Color.green, Color.red);

            Assert.AreEqual(Color.green, converter.Convert(true));
            Assert.AreEqual(Color.red, converter.Convert(false));
        }

        [Test]
        public void ConvertBack_ReadsTheAuthoredBranchBack()
        {
            var converter = new BoolToValueConverter<Color>(Color.green, Color.red);

            Assert.IsTrue(converter.ConvertBack(Color.green));
            Assert.IsFalse(converter.ConvertBack(Color.red));
        }

        [Test]
        public void ConvertBack_UnmatchedValue_ReturnsFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("one of the two authored values"));

            var converter = new BoolToValueConverter<Color>(Color.green, Color.red, convertBackFallback: true);

            Assert.IsTrue(converter.ConvertBack(Color.blue));
            Assert.IsTrue(converter.ConvertBack(Color.blue));
        }

        [Test]
        public void ConvertBack_BranchesAuthoredAlike_ReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("both branches hold"));

            var converter = new BoolToValueConverter<Color>(Color.green, Color.green, convertBackFallback: true);

            Assert.IsTrue(converter.ConvertBack(Color.green));
            Assert.IsTrue(converter.ConvertBack(Color.green));
        }

        // A value matching neither branch cannot be read back, so the authored fallback answers.
        [Test]
        public void ConvertBack_ValueMatchingNeitherBranch_ReportsAndUsesTheFallback()
        {
            var converter = new BoolToValueConverter<object>("a", "b", convertBackFallback: true);

            LogAssert.Expect(LogType.Error, new Regex("BoolToValueConverter<object>.*Using the fallback"));

            Assert.IsTrue(converter.ConvertBack("neither"));
        }
    }
}
