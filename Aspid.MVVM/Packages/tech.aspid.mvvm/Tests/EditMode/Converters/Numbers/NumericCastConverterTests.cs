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
    /// Coverage for <see cref="NumericCastConverter"/> — the three <see cref="OverflowMode"/>
    /// policies over the six narrowing conversions, the six widening ones, and the boundary of
    /// every target type.
    /// </summary>
    /// <remarks>
    /// A bare <c>(int)</c> cast turns <see cref="long.MaxValue"/> into <c>-1</c>, so the load-bearing
    /// rows are the ones pinning each mode's answer to a value the target cannot hold. All twelve
    /// conversions are explicit interface members, which is why every call below goes through a cast.
    /// <para>
    /// Nothing asserts an <see cref="OverflowMode.Unchecked"/> floating-point narrowing that lands out
    /// of range: the specification calls that result unspecified and the runtimes disagree.
    /// </para>
    /// </remarks>
    [TestFixture]
    public sealed class NumericCastConverterTests
    {
        // Only reachable from a serialized asset written by a newer package version, or from
        // hand-edited YAML. Every narrowing path has to report it and saturate rather than fall through.
        private const OverflowMode UndeclaredMode = (OverflowMode)42;

        #region The case the audit named: long.MaxValue into an int
        // A long score pushed at an int binder. Three modes, three different answers, and the
        // answer a plain cast gives is the one that renders a progress bar backwards.
        [Test]
        public void Convert_LongMaxValueToInt_Saturate_ReturnsIntMaxValue() =>
            Assert.AreEqual(int.MaxValue, LongToInt(OverflowMode.Saturate).Convert(long.MaxValue));

        [Test]
        public void Convert_LongMaxValueToInt_Unchecked_ReturnsMinusOne() =>
            Assert.AreEqual(-1, LongToInt(OverflowMode.Unchecked).Convert(long.MaxValue));

        [Test]
        public void Convert_LongMaxValueToInt_Checked_Throws() =>
            Assert.Throws<OverflowException>(() => LongToInt(OverflowMode.Checked).Convert(long.MaxValue));

        // The field initializer is the whole safety story: a converter dropped into the inspector
        // and left alone must not wrap. OverflowMode.Unchecked is the zero value, so anything that
        // bypasses the initializer silently selects the dangerous mode.
        [Test]
        public void Convert_DefaultConstructed_SaturatesRatherThanWrapping() =>
            Assert.AreEqual(int.MaxValue, ((IConverter<long, int>)new NumericCastConverter()).Convert(long.MaxValue));
        #endregion

        #region long to int
        // 4294967296 is 2^32: every low bit is zero, so an unchecked cast reports a full-precision
        // score as nothing at all. It is the case that makes wrapping look like a data-loss bug
        // rather than an arithmetic one.
        [TestCase(long.MaxValue, int.MaxValue)]
        [TestCase(long.MinValue, int.MinValue)]
        [TestCase(2147483648L, int.MaxValue)]
        [TestCase(-2147483649L, int.MinValue)]
        [TestCase(4294967296L, int.MaxValue)]
        [TestCase(2147483647L, int.MaxValue)]
        [TestCase(-2147483648L, int.MinValue)]
        [TestCase(0L, 0)]
        [TestCase(42L, 42)]
        public void Convert_LongToInt_Saturate_ClampsToTheIntBounds(long value, int expected) =>
            Assert.AreEqual(expected, LongToInt(OverflowMode.Saturate).Convert(value));

        [TestCase(long.MaxValue, -1)]
        [TestCase(long.MinValue, 0)]
        [TestCase(2147483648L, int.MinValue)]
        [TestCase(-2147483649L, int.MaxValue)]
        [TestCase(4294967296L, 0)]
        [TestCase(42L, 42)]
        public void Convert_LongToInt_Unchecked_KeepsTheLowThirtyTwoBits(long value, int expected) =>
            Assert.AreEqual(expected, LongToInt(OverflowMode.Unchecked).Convert(value));

        // The bounds themselves have to survive Checked untouched — an off-by-one in the range test
        // would make a legitimate int.MaxValue throw.
        [TestCase(2147483647L, int.MaxValue)]
        [TestCase(-2147483648L, int.MinValue)]
        [TestCase(0L, 0)]
        public void Convert_LongToInt_Checked_PassesValuesThatFit(long value, int expected) =>
            Assert.AreEqual(expected, LongToInt(OverflowMode.Checked).Convert(value));

        [TestCase(long.MaxValue)]
        [TestCase(long.MinValue)]
        [TestCase(2147483648L)]
        [TestCase(-2147483649L)]
        [TestCase(4294967296L)]
        public void Convert_LongToInt_Checked_ThrowsForValuesThatDoNotFit(long value) =>
            Assert.Throws<OverflowException>(() => LongToInt(OverflowMode.Checked).Convert(value));
        #endregion

        #region double to int
        // NaN is checked before the bounds because it fails every comparison; if that order is ever
        // flipped it falls through to the undefined cast this class exists to avoid. 2147483646.9
        // is the in-range control that proves the fraction is still dropped toward zero rather than
        // rounded.
        [TestCase(double.NaN, 0)]
        [TestCase(double.PositiveInfinity, int.MaxValue)]
        [TestCase(double.NegativeInfinity, int.MinValue)]
        [TestCase(1e18d, int.MaxValue)]
        [TestCase(-1e18d, int.MinValue)]
        [TestCase(2147483647.9d, int.MaxValue)]
        [TestCase(-2147483648.9d, int.MinValue)]
        [TestCase(2147483646.9d, 2147483646)]
        [TestCase(2.9d, 2)]
        [TestCase(-2.9d, -2)]
        [TestCase(0d, 0)]
        public void Convert_DoubleToInt_Saturate_ClampsToTheIntBoundsAndZeroesNaN(double value, int expected) =>
            Assert.AreEqual(expected, DoubleToInt(OverflowMode.Saturate).Convert(value));

        // Only the in-range half of Unchecked is specified for a floating-point source; see the
        // fixture remarks for why the out-of-range half is deliberately absent.
        [TestCase(2.9d, 2)]
        [TestCase(-2.9d, -2)]
        [TestCase(2147483647d, int.MaxValue)]
        [TestCase(-2147483648d, int.MinValue)]
        public void Convert_DoubleToInt_Unchecked_TruncatesTowardZeroWhenItFits(double value, int expected) =>
            Assert.AreEqual(expected, DoubleToInt(OverflowMode.Unchecked).Convert(value));

        [TestCase(2.9d, 2)]
        [TestCase(-2.9d, -2)]
        [TestCase(2147483647d, int.MaxValue)]
        [TestCase(-2147483648d, int.MinValue)]
        public void Convert_DoubleToInt_Checked_TruncatesTowardZeroWhenItFits(double value, int expected) =>
            Assert.AreEqual(expected, DoubleToInt(OverflowMode.Checked).Convert(value));

        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(1e18d)]
        [TestCase(-1e18d)]
        [TestCase(2147483648d)]
        [TestCase(-2147483649d)]
        public void Convert_DoubleToInt_Checked_ThrowsForValuesThatDoNotFit(double value) =>
            Assert.Throws<OverflowException>(() => DoubleToInt(OverflowMode.Checked).Convert(value));
        #endregion

        #region double to long
        // 9223372036854775808 is 2^63, the nearest double to long.MaxValue and one above it — the
        // literal the source tests against instead of long.MaxValue. 9223372036854774784 is the
        // largest double strictly below it and must still pass through unclamped; if the boundary
        // ever drifts by one ulp this pair is what catches it.
        [TestCase(double.NaN, 0L)]
        [TestCase(double.PositiveInfinity, long.MaxValue)]
        [TestCase(double.NegativeInfinity, long.MinValue)]
        [TestCase(1e19d, long.MaxValue)]
        [TestCase(-1e19d, long.MinValue)]
        [TestCase(9223372036854775808d, long.MaxValue)]
        [TestCase(-9223372036854775808d, long.MinValue)]
        [TestCase(9223372036854774784d, 9223372036854774784L)]
        [TestCase(1e15d, 1000000000000000L)]
        [TestCase(3.9d, 3L)]
        [TestCase(-3.9d, -3L)]
        [TestCase(0d, 0L)]
        public void Convert_DoubleToLong_Saturate_ClampsToTheLongBoundsAndZeroesNaN(double value, long expected) =>
            Assert.AreEqual(expected, DoubleToLong(OverflowMode.Saturate).Convert(value));

        [TestCase(3.9d, 3L)]
        [TestCase(-3.9d, -3L)]
        [TestCase(1e15d, 1000000000000000L)]
        public void Convert_DoubleToLong_Unchecked_TruncatesTowardZeroWhenItFits(double value, long expected) =>
            Assert.AreEqual(expected, DoubleToLong(OverflowMode.Unchecked).Convert(value));

        // long.MinValue is exactly -2^63 and a double holds it exactly; long.MaxValue is 2^63 - 1
        // and no double holds it at all. So the checked range is lop-sided: the negative bound is
        // reachable and the positive one is a rounding away from being an overflow.
        [Test]
        public void Convert_DoubleToLong_Checked_AcceptsTheNegativeBoundButNotThePositiveOne()
        {
            Assert.AreEqual(long.MinValue, DoubleToLong(OverflowMode.Checked).Convert(-9223372036854775808d));
            Assert.Throws<OverflowException>(() => DoubleToLong(OverflowMode.Checked).Convert(9223372036854775808d));
        }

        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(1e19d)]
        [TestCase(-1e19d)]
        public void Convert_DoubleToLong_Checked_ThrowsForValuesThatDoNotFit(double value) =>
            Assert.Throws<OverflowException>(() => DoubleToLong(OverflowMode.Checked).Convert(value));
        #endregion

        #region float to int and float to long
        // The float overloads forward to the double helpers, so a float source is widened first and
        // its whole range is out of int range at the top. float.MinValue is the most negative float,
        // not the smallest positive one, which is why it saturates downward.
        [TestCase(float.NaN, 0)]
        [TestCase(float.PositiveInfinity, int.MaxValue)]
        [TestCase(float.NegativeInfinity, int.MinValue)]
        [TestCase(float.MaxValue, int.MaxValue)]
        [TestCase(float.MinValue, int.MinValue)]
        [TestCase(3e9f, int.MaxValue)]
        [TestCase(3.7f, 3)]
        [TestCase(-3.7f, -3)]
        public void Convert_FloatToInt_Saturate_ClampsToTheIntBoundsAndZeroesNaN(float value, int expected) =>
            Assert.AreEqual(expected, FloatToInt(OverflowMode.Saturate).Convert(value));

        [TestCase(float.NaN, 0L)]
        [TestCase(float.PositiveInfinity, long.MaxValue)]
        [TestCase(float.NegativeInfinity, long.MinValue)]
        [TestCase(float.MaxValue, long.MaxValue)]
        [TestCase(float.MinValue, long.MinValue)]
        [TestCase(5.9f, 5L)]
        [TestCase(-5.9f, -5L)]
        public void Convert_FloatToLong_Saturate_ClampsToTheLongBoundsAndZeroesNaN(float value, long expected) =>
            Assert.AreEqual(expected, FloatToLong(OverflowMode.Saturate).Convert(value));

        // No float holds int.MaxValue: the nearest is 2^31, one larger than any int. Saturate lands
        // back on int.MaxValue so a widen-then-narrow round trip looks lossless, while Checked calls
        // the identical value an overflow. Widening then narrowing is only an identity in Saturate.
        [Test]
        public void Convert_IntMaxValueThroughFloat_Saturate_ComesBackAsIntMaxValue() =>
            Assert.AreEqual(int.MaxValue, FloatToInt(OverflowMode.Saturate).Convert(int.MaxValue));

        [Test]
        public void Convert_IntMaxValueThroughFloat_Checked_Throws() =>
            Assert.Throws<OverflowException>(() => FloatToInt(OverflowMode.Checked).Convert(int.MaxValue));

        [Test]
        public void Convert_LongMaxValueThroughFloat_Saturate_ComesBackAsLongMaxValue() =>
            Assert.AreEqual(long.MaxValue, FloatToLong(OverflowMode.Saturate).Convert(long.MaxValue));

        [Test]
        public void Convert_LongMaxValueThroughFloat_Checked_Throws() =>
            Assert.Throws<OverflowException>(() => FloatToLong(OverflowMode.Checked).Convert(long.MaxValue));
        #endregion

        #region double to float
        [TestCase(1e39d, float.MaxValue)]
        [TestCase(-1e39d, float.MinValue)]
        [TestCase(double.MaxValue, float.MaxValue)]
        [TestCase(double.MinValue, float.MinValue)]
        [TestCase(3.4028234663852886e38d, float.MaxValue)]
        [TestCase(-3.4028234663852886e38d, float.MinValue)]
        [TestCase(1.5d, 1.5f)]
        [TestCase(0d, 0f)]
        public void Convert_DoubleToFloat_Saturate_ClampsToTheFloatBounds(double value, float expected) =>
            Assert.AreEqual(expected, DoubleToFloat(OverflowMode.Saturate).Convert(value));

        // A NaN keeps being a NaN here rather than becoming zero the way it does on an integer
        // target: it carries no magnitude to saturate toward and a float can represent it.
        [Test]
        public void Convert_DoubleToFloat_Saturate_KeepsNaN() =>
            Assert.IsTrue(float.IsNaN(DoubleToFloat(OverflowMode.Saturate).Convert(double.NaN)));

        // Documented behavior and actual behavior part company here. A float can represent an
        // infinity just as well as it represents a NaN, but the `value >= float.MaxValue` bound test
        // catches the infinity before the cast, so Saturate replaces it with a finite number while
        // Unchecked and Checked both hand it back. Asserted so the asymmetry is on the record.
        [Test]
        public void Convert_DoubleToFloat_Saturate_KeepsPositiveInfinity() =>
            Assert.AreEqual(float.PositiveInfinity, DoubleToFloat(OverflowMode.Saturate).Convert(double.PositiveInfinity));

        [Test]
        public void Convert_DoubleToFloat_Saturate_KeepsNegativeInfinity() =>
            Assert.AreEqual(float.NegativeInfinity, DoubleToFloat(OverflowMode.Saturate).Convert(double.NegativeInfinity));

        // The one narrowing IEEE-754 pins down, so Unchecked has a defined answer here where it has
        // none for an integer target: overflow becomes an infinity, not a wrapped bit pattern.
        [Test]
        public void Convert_DoubleToFloat_Unchecked_OverflowBecomesAnInfinity()
        {
            Assert.IsTrue(float.IsPositiveInfinity(DoubleToFloat(OverflowMode.Unchecked).Convert(1e39d)));
            Assert.IsTrue(float.IsNegativeInfinity(DoubleToFloat(OverflowMode.Unchecked).Convert(-1e39d)));
        }

        [Test]
        public void Convert_DoubleToFloat_Unchecked_KeepsAnInfinityThatArrivedAsOne() =>
            Assert.IsTrue(float.IsPositiveInfinity(DoubleToFloat(OverflowMode.Unchecked).Convert(double.PositiveInfinity)));

        // checked() emits nothing for a double-to-float narrowing, so the range test is hand-written
        // as "finite in, infinite out". An infinity that arrived as one is not an overflow this
        // conversion caused, so it passes through untouched.
        [Test]
        public void Convert_DoubleToFloat_Checked_PassesAnInfinityThroughButRejectsAFiniteOverflow()
        {
            Assert.IsTrue(float.IsPositiveInfinity(DoubleToFloat(OverflowMode.Checked).Convert(double.PositiveInfinity)));
            Assert.IsTrue(float.IsNegativeInfinity(DoubleToFloat(OverflowMode.Checked).Convert(double.NegativeInfinity)));
            Assert.Throws<OverflowException>(() => DoubleToFloat(OverflowMode.Checked).Convert(1e39d));
            Assert.Throws<OverflowException>(() => DoubleToFloat(OverflowMode.Checked).Convert(double.MaxValue));
        }

        // Documented behavior and actual behavior part company again. Checked is described as
        // throwing for a value the target cannot hold, but the hand-written test only looks at the
        // top of the range: a double too small for a float underflows to a clean zero in every mode,
        // silently, which is exactly the class of loss Checked is chosen to prevent.
        [TestCase(OverflowMode.Unchecked)]
        [TestCase(OverflowMode.Checked)]
        [TestCase(OverflowMode.Saturate)]
        public void Convert_DoubleToFloat_Underflow_ReturnsZeroWithoutThrowing(OverflowMode mode) =>
            Assert.AreEqual(0f, DoubleToFloat(mode).Convert(1e-50d));

        // checked() does cover the integral conversions, so the same NaN under the same mode throws
        // for an int target and survives for a float one.
        [Test]
        public void Convert_NaN_Checked_ThrowsForAnIntegerTargetButNotForAFloatOne()
        {
            Assert.Throws<OverflowException>(() => DoubleToInt(OverflowMode.Checked).Convert(double.NaN));
            Assert.IsTrue(float.IsNaN(DoubleToFloat(OverflowMode.Checked).Convert(double.NaN)));
        }
        #endregion

        #region Widening
        [TestCase(int.MaxValue, 2147483647L)]
        [TestCase(int.MinValue, -2147483648L)]
        [TestCase(0, 0L)]
        public void Convert_IntToLong_IsExact(int value, long expected) =>
            Assert.AreEqual(expected, IntToLong(OverflowMode.Saturate).Convert(value));

        [TestCase(int.MaxValue, 2147483647d)]
        [TestCase(int.MinValue, -2147483648d)]
        [TestCase(16777217, 16777217d)]
        public void Convert_IntToDouble_IsExact(int value, double expected) =>
            Assert.AreEqual(expected, IntToDouble(OverflowMode.Saturate).Convert(value));

        // Widening cannot overflow, which is not the same as cannot change the number. 2^24 + 1 is
        // the first int a float cannot name, and int.MaxValue rounds to 2^31 — larger than any int
        // that could have produced it. A test asserting int.MaxValue here would be asserting a
        // rounding that does not happen.
        [TestCase(0, 0f)]
        [TestCase(16777216, 16777216f)]
        [TestCase(16777217, 16777216f)]
        [TestCase(int.MaxValue, 2147483648f)]
        public void Convert_IntToFloat_RoundsToTheNearestRepresentableFloat(int value, float expected) =>
            Assert.AreEqual(expected, IntToFloat(OverflowMode.Saturate).Convert(value));

        [TestCase(0L, 0f)]
        [TestCase(long.MaxValue, 9223372036854775808f)]
        public void Convert_LongToFloat_RoundsToTheNearestRepresentableFloat(long value, float expected) =>
            Assert.AreEqual(expected, LongToFloat(OverflowMode.Saturate).Convert(value));

        // 2^53 + 1 is the first long a double cannot name; long.MaxValue rounds up to 2^63, one
        // above the largest long, which is why the saturating helper tests against that literal
        // rather than against long.MaxValue itself.
        [TestCase(0L, 0d)]
        [TestCase(9007199254740992L, 9007199254740992d)]
        [TestCase(9007199254740993L, 9007199254740992d)]
        [TestCase(long.MaxValue, 9223372036854775808d)]
        public void Convert_LongToDouble_RoundsToTheNearestRepresentableDouble(long value, double expected) =>
            Assert.AreEqual(expected, LongToDouble(OverflowMode.Saturate).Convert(value));

        // Widening a float does not clean it up: 0.1f was never 0.1, and the double it becomes says
        // so. Asserting 0.1d here would be asserting a correction the conversion never makes.
        [Test]
        public void Convert_FloatToDouble_KeepsTheFloatsExactValue() =>
            Assert.AreEqual(0.10000000149011612d, FloatToDouble(OverflowMode.Saturate).Convert(0.1f));

        [Test]
        public void Convert_FloatToDouble_KeepsNaN() =>
            Assert.IsTrue(double.IsNaN(FloatToDouble(OverflowMode.Saturate).Convert(float.NaN)));

        [Test]
        public void Convert_FloatToDouble_KeepsAnInfinity() =>
            Assert.IsTrue(double.IsPositiveInfinity(FloatToDouble(OverflowMode.Saturate).Convert(float.PositiveInfinity)));

        // The widening paths never read the mode — there is no switch on them at all. If one is ever
        // routed through the shared helper "for consistency", an undeclared mode starts reporting
        // where it used to be ignored, and these are the tests that notice.
        [Test]
        public void Convert_Widening_IgnoresTheModeEntirely()
        {
            Assert.AreEqual(2147483647L, IntToLong(UndeclaredMode).Convert(int.MaxValue));
            Assert.AreEqual(2147483648f, IntToFloat(UndeclaredMode).Convert(int.MaxValue));
            Assert.AreEqual(2147483647d, IntToDouble(UndeclaredMode).Convert(int.MaxValue));
            Assert.AreEqual(9223372036854775808f, LongToFloat(UndeclaredMode).Convert(long.MaxValue));
            Assert.AreEqual(9223372036854775808d, LongToDouble(UndeclaredMode).Convert(long.MaxValue));
            Assert.AreEqual(1.5d, FloatToDouble(UndeclaredMode).Convert(1.5f));
        }
        #endregion

        #region Contract
        // An undeclared mode is a misconfiguration, not data: it is reported on every push and
        // answered with the default policy, the only one whose result is defined everywhere.
        [Test]
        public void Convert_UndeclaredMode_LongToInt_ReportsAndSaturates()
        {
            ExpectUndeclaredMode();

            Assert.AreEqual(int.MaxValue, LongToInt(UndeclaredMode).Convert(long.MaxValue));
        }

        [Test]
        public void Convert_UndeclaredMode_DoubleToInt_ReportsAndSaturates()
        {
            ExpectUndeclaredMode();

            Assert.AreEqual(int.MaxValue, DoubleToInt(UndeclaredMode).Convert(1e30d));
        }

        [Test]
        public void Convert_UndeclaredMode_DoubleToLong_ReportsAndSaturates()
        {
            ExpectUndeclaredMode();

            Assert.AreEqual(long.MaxValue, DoubleToLong(UndeclaredMode).Convert(1e30d));
        }

        [Test]
        public void Convert_UndeclaredMode_DoubleToFloat_ReportsAndSaturates()
        {
            ExpectUndeclaredMode();

            Assert.AreEqual(float.MaxValue, DoubleToFloat(UndeclaredMode).Convert(double.MaxValue));
        }

        private static void ExpectUndeclaredMode() =>
            LogAssert.Expect(LogType.Error, new Regex("NumericCastConverter.*not a declared OverflowMode"));

        // A picker entry that silently lost one of its twelve pairs would fail at bind time in a
        // build, not here, so the interface list is asserted rather than assumed. The self-pairs
        // (int to int and friends) are deliberately absent — they would be entries that do nothing.
        [TestCase(typeof(IConverter<int, long>))]
        [TestCase(typeof(IConverter<int, float>))]
        [TestCase(typeof(IConverter<int, double>))]
        [TestCase(typeof(IConverter<long, int>))]
        [TestCase(typeof(IConverter<long, float>))]
        [TestCase(typeof(IConverter<long, double>))]
        [TestCase(typeof(IConverter<float, int>))]
        [TestCase(typeof(IConverter<float, long>))]
        [TestCase(typeof(IConverter<float, double>))]
        [TestCase(typeof(IConverter<double, int>))]
        [TestCase(typeof(IConverter<double, long>))]
        [TestCase(typeof(IConverter<double, float>))]
        public void NumericCastConverter_ImplementsEveryCrossTypePair(Type pair) =>
            Assert.IsTrue(pair.IsInstanceOfType(new NumericCastConverter()));

        // The mode is read on every call and nothing is cached between them, so one instance shared
        // by several binders must not let the first value it sees change the next answer.
        [Test]
        public void Convert_SameInstance_ReusedAcrossValues_IsStateless()
        {
            var converter = LongToInt(OverflowMode.Saturate);

            Assert.AreEqual(int.MaxValue, converter.Convert(long.MaxValue));
            Assert.AreEqual(42, converter.Convert(42L));
            Assert.AreEqual(int.MinValue, converter.Convert(long.MinValue));
            Assert.AreEqual(42, converter.Convert(42L));
        }
        #endregion

        private static IConverter<long, int> LongToInt(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<double, int> DoubleToInt(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<float, int> FloatToInt(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<double, long> DoubleToLong(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<float, long> FloatToLong(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<double, float> DoubleToFloat(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<int, long> IntToLong(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<int, float> IntToFloat(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<int, double> IntToDouble(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<long, float> LongToFloat(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<long, double> LongToDouble(OverflowMode mode) =>
            new NumericCastConverter(mode);

        private static IConverter<float, double> FloatToDouble(OverflowMode mode) =>
            new NumericCastConverter(mode);
    }
}
