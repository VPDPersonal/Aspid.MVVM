using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="NumberCompareConverter"/> across all six <see cref="ComparisonMode"/>
    /// members and all four numeric overloads.
    /// </summary>
    /// <remarks>
    /// The magnitude cases at the bottom cover the tolerance: it is shared by all six comparisons and
    /// sized after the incoming type, so the same pair of numbers can differ as doubles and match as
    /// floats.
    /// </remarks>
    [TestFixture]
    internal sealed class NumberCompareConverterTests
    {
        [TestCase(ComparisonMode.LessThan, 5f, true)]
        [TestCase(ComparisonMode.LessThan, 10f, false)]
        [TestCase(ComparisonMode.LessThan, 15f, false)]
        [TestCase(ComparisonMode.GreaterThan, 5f, false)]
        [TestCase(ComparisonMode.GreaterThan, 10f, false)]
        [TestCase(ComparisonMode.GreaterThan, 15f, true)]
        [TestCase(ComparisonMode.LessThanOrEqual, 5f, true)]
        [TestCase(ComparisonMode.LessThanOrEqual, 10f, true)]
        [TestCase(ComparisonMode.LessThanOrEqual, 15f, false)]
        [TestCase(ComparisonMode.GreaterThanOrEqual, 5f, false)]
        [TestCase(ComparisonMode.GreaterThanOrEqual, 10f, true)]
        [TestCase(ComparisonMode.GreaterThanOrEqual, 15f, true)]
        [TestCase(ComparisonMode.Equal, 5f, false)]
        [TestCase(ComparisonMode.Equal, 10f, true)]
        [TestCase(ComparisonMode.Equal, 15f, false)]
        [TestCase(ComparisonMode.NotEqual, 5f, true)]
        [TestCase(ComparisonMode.NotEqual, 10f, false)]
        [TestCase(ComparisonMode.NotEqual, 15f, true)]
        public void Convert_Float_MatchesTheComparison(ComparisonMode comparison, float value, bool expected) =>
            Assert.AreEqual(expected, new NumberCompareConverter(comparison, value: 10f).Convert(value));

        [TestCase(ComparisonMode.LessThan, 5, true)]
        [TestCase(ComparisonMode.GreaterThan, 15, true)]
        [TestCase(ComparisonMode.Equal, 10, true)]
        [TestCase(ComparisonMode.NotEqual, 11, true)]
        public void Convert_Int_MatchesTheComparison(ComparisonMode comparison, int value, bool expected) =>
            Assert.AreEqual(expected, new NumberCompareConverter(comparison, value: 10f).Convert(value));

        [TestCase(ComparisonMode.LessThan, 5L, true)]
        [TestCase(ComparisonMode.GreaterThan, 15L, true)]
        [TestCase(ComparisonMode.Equal, 10L, true)]
        [TestCase(ComparisonMode.NotEqual, 11L, true)]
        public void Convert_Long_MatchesTheComparison(ComparisonMode comparison, long value, bool expected) =>
            Assert.AreEqual(expected, new NumberCompareConverter(comparison, value: 10f).Convert(value));

        [TestCase(ComparisonMode.LessThan, 5d, true)]
        [TestCase(ComparisonMode.GreaterThan, 15d, true)]
        [TestCase(ComparisonMode.Equal, 10d, true)]
        [TestCase(ComparisonMode.NotEqual, 11d, true)]
        public void Convert_Double_MatchesTheComparison(ComparisonMode comparison, double value, bool expected) =>
            Assert.AreEqual(expected, new NumberCompareConverter(comparison, value: 10f).Convert(value));

        [Test]
        public void Convert_DefaultConstructed_ComparesAgainstZeroWithEqual() =>
            Assert.IsTrue(new NumberCompareConverter().Convert(0f));

        // The float tolerance is 1e-6 * magnitude, so at 2e6 anything within ~2.0 is the same float.
        [Test]
        public void Convert_Equal_LargeMagnitudes_AreWithinTheRelativeTolerance() =>
            Assert.IsTrue(new NumberCompareConverter(ComparisonMode.Equal, value: 2_000_000f).Convert(2_000_001f));

        [Test]
        public void Convert_Inequality_LargeMagnitudes_AreWithinTheRelativeTolerance() =>
            Assert.IsFalse(new NumberCompareConverter(ComparisonMode.NotEqual, value: 2_000_000f).Convert(2_000_001f));

        // The tolerance applies to every comparison, so the six agree at the boundary: a value the
        // converter calls equal is neither greater than nor less than the target.
        [Test]
        public void Convert_ToleranceIsAppliedToOrderingComparisons()
        {
            const float target = 2_000_000f;
            const float value = 2_000_001f;

            Assert.IsTrue(new NumberCompareConverter(ComparisonMode.Equal, target).Convert(value));
            Assert.IsFalse(new NumberCompareConverter(ComparisonMode.GreaterThan, target).Convert(value));
            Assert.IsTrue(new NumberCompareConverter(ComparisonMode.LessThanOrEqual, target).Convert(value));
        }

        // A double is measured against 1e-12 * magnitude — a millionth of what the same pair gets
        // as floats.
        [Test]
        public void Convert_Double_UsesATighterToleranceThanFloat()
        {
            Assert.IsFalse(new NumberCompareConverter(ComparisonMode.Equal, value: 2_000_000d).Convert(2_000_001d));
            Assert.IsTrue(new NumberCompareConverter(ComparisonMode.Equal, value: 2_000_000d).Convert(2_000_000.000000001d));
        }

        [Test]
        public void Convert_Int_ComparesExactly() =>
            Assert.IsFalse(new NumberCompareConverter(ComparisonMode.Equal, value: 2_000_000d).Convert(2_000_001));

        [Test]
        public void Convert_Long_ComparesExactly() =>
            Assert.IsFalse(new NumberCompareConverter(ComparisonMode.Equal, value: 2_000_000d).Convert(2_000_001L));

        [TestCase(ComparisonMode.Equal, false)]
        [TestCase(ComparisonMode.LessThan, true)]
        [TestCase(ComparisonMode.GreaterThan, false)]
        public void Convert_Int_AgainstAFractionalTarget(ComparisonMode comparison, bool expected) =>
            Assert.AreEqual(expected, new NumberCompareConverter(comparison, value: 3.5d).Convert(3));
    }
}
