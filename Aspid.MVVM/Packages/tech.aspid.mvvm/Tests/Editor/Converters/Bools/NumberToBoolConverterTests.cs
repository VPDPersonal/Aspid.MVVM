using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="NumberToBoolConverter"/> across all six <see cref="Comparisons"/>
    /// and all four numeric overloads.
    /// </summary>
    /// <remarks>
    /// <see cref="Comparisons.Equal"/> and <see cref="Comparisons.Inequality"/> are deliberately
    /// fuzzy — they route through a relative 1e-6 tolerance rather than <c>==</c>. The magnitude
    /// cases at the bottom pin that behaviour down as it stands today; they are characterisation,
    /// not endorsement.
    /// </remarks>
    [TestFixture]
    internal sealed class NumberToBoolConverterTests
    {
        [TestCase(Comparisons.LessThan, 5f, true)]
        [TestCase(Comparisons.LessThan, 10f, false)]
        [TestCase(Comparisons.LessThan, 15f, false)]
        [TestCase(Comparisons.GreaterThan, 5f, false)]
        [TestCase(Comparisons.GreaterThan, 10f, false)]
        [TestCase(Comparisons.GreaterThan, 15f, true)]
        [TestCase(Comparisons.LessThanOrEqual, 5f, true)]
        [TestCase(Comparisons.LessThanOrEqual, 10f, true)]
        [TestCase(Comparisons.LessThanOrEqual, 15f, false)]
        [TestCase(Comparisons.GreaterThanOrEqual, 5f, false)]
        [TestCase(Comparisons.GreaterThanOrEqual, 10f, true)]
        [TestCase(Comparisons.GreaterThanOrEqual, 15f, true)]
        [TestCase(Comparisons.Equal, 5f, false)]
        [TestCase(Comparisons.Equal, 10f, true)]
        [TestCase(Comparisons.Equal, 15f, false)]
        [TestCase(Comparisons.Inequality, 5f, true)]
        [TestCase(Comparisons.Inequality, 10f, false)]
        [TestCase(Comparisons.Inequality, 15f, true)]
        public void Convert_Float_MatchesTheComparison(Comparisons comparison, float value, bool expected) =>
            Assert.AreEqual(expected, new NumberToBoolConverter(comparison, value: 10f).Convert(value));

        [TestCase(Comparisons.LessThan, 5, true)]
        [TestCase(Comparisons.GreaterThan, 15, true)]
        [TestCase(Comparisons.Equal, 10, true)]
        [TestCase(Comparisons.Inequality, 11, true)]
        public void Convert_Int_MatchesTheComparison(Comparisons comparison, int value, bool expected) =>
            Assert.AreEqual(expected, new NumberToBoolConverter(comparison, value: 10f).Convert(value));

        [TestCase(Comparisons.LessThan, 5L, true)]
        [TestCase(Comparisons.GreaterThan, 15L, true)]
        [TestCase(Comparisons.Equal, 10L, true)]
        [TestCase(Comparisons.Inequality, 11L, true)]
        public void Convert_Long_MatchesTheComparison(Comparisons comparison, long value, bool expected) =>
            Assert.AreEqual(expected, new NumberToBoolConverter(comparison, value: 10f).Convert(value));

        [TestCase(Comparisons.LessThan, 5d, true)]
        [TestCase(Comparisons.GreaterThan, 15d, true)]
        [TestCase(Comparisons.Equal, 10d, true)]
        [TestCase(Comparisons.Inequality, 11d, true)]
        public void Convert_Double_MatchesTheComparison(Comparisons comparison, double value, bool expected) =>
            Assert.AreEqual(expected, new NumberToBoolConverter(comparison, value: 10f).Convert(value));

        [Test]
        public void Convert_DefaultConstructed_ComparesAgainstZeroWithEqual() =>
            Assert.IsTrue(new NumberToBoolConverter().Convert(0f));

        // Characterisation of a known defect, not a desired contract: the relative tolerance is
        // 1e-6 * magnitude, so at 2e6 anything within ~2.0 reads as equal. Tracked in the audit
        // as a LOW finding; deliberately left alone by the Phase 0 batch.
        [Test]
        public void Convert_Equal_LargeMagnitudes_AreWithinTheRelativeTolerance() =>
            Assert.IsTrue(new NumberToBoolConverter(Comparisons.Equal, value: 2_000_000f).Convert(2_000_001f));

        [Test]
        public void Convert_Inequality_LargeMagnitudes_AreWithinTheRelativeTolerance() =>
            Assert.IsFalse(new NumberToBoolConverter(Comparisons.Inequality, value: 2_000_000f).Convert(2_000_001f));

        // The tolerance applies to Equal/Inequality only, so the predicate set is mutually
        // inconsistent at the boundary: a value can be "equal to" and "greater than" at once.
        [Test]
        public void Convert_ToleranceIsNotAppliedToOrderingComparisons()
        {
            const float target = 2_000_000f;
            const float value = 2_000_001f;

            Assert.IsTrue(new NumberToBoolConverter(Comparisons.Equal, target).Convert(value));
            Assert.IsTrue(new NumberToBoolConverter(Comparisons.GreaterThan, target).Convert(value));
            Assert.IsFalse(new NumberToBoolConverter(Comparisons.LessThanOrEqual, target).Convert(value));
        }
    }
}
