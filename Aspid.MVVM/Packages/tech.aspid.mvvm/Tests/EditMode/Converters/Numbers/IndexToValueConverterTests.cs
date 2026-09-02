using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="IndexToValueConverter{T}"/> — the three out-of-range modes, the empty
    /// array, and the undeclared-mode fallback.
    /// </summary>
    [TestFixture]
    public sealed class IndexToValueConverterTests
    {
        private static readonly string[] _values = { "a", "b", "c" };

        [TestCase(0, "a")]
        [TestCase(1, "b")]
        [TestCase(2, "c")]
        public void Convert_InRange_PicksTheValue(int index, string expected) =>
            Assert.AreEqual(expected, new IndexToValueConverter<string>(_values).Convert(index));

        [TestCase(-1, "a")]
        [TestCase(99, "c")]
        public void Convert_Clamp_UsesTheNearestEnd(int index, string expected) =>
            Assert.AreEqual(expected, new IndexToValueConverter<string>(_values, IndexMode.Clamp).Convert(index));

        [TestCase(-1, "c")]
        [TestCase(3, "a")]
        [TestCase(4, "b")]
        public void Convert_Wrap_FoldsAroundTheArray(int index, string expected) =>
            Assert.AreEqual(expected, new IndexToValueConverter<string>(_values, IndexMode.Wrap).Convert(index));

        [Test]
        public void Convert_Fallback_ReturnsTheFallbackOutOfRange() =>
            Assert.AreEqual(
                "z",
                new IndexToValueConverter<string>(_values, IndexMode.Fallback, "z").Convert(99));

        [Test]
        public void Convert_EmptyArray_ReportsAndReturnsTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("IndexToValueConverter.*no values are authored"));

            Assert.AreEqual("z", new IndexToValueConverter<string>(null, fallback: "z").Convert(0));
        }

        [Test]
        public void Convert_UndeclaredMode_ReportsAndReturnsTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("IndexToValueConverter.*not a declared IndexMode"));

            Assert.AreEqual(
                "z",
                new IndexToValueConverter<string>(_values, (IndexMode)99, "z").Convert(99));
        }

        // An index inside the array returns before the switch is reached, so a broken mode stays
        // invisible until the first out-of-range value arrives.
        [Test]
        public void Convert_UndeclaredMode_IsNotReachedForAnIndexInsideTheArray() =>
            Assert.AreEqual("b", new IndexToValueConverter<string>(_values, (IndexMode)99, "z").Convert(1));

        [Test]
        public void IndexToValue_Long_PicksTheSameValue() =>
            Assert.AreEqual(
                "b",
                ((IConverter<long, string>)new IndexToValueConverter<string>(new[] { "a", "b", "c" })).Convert(1L));

        // A position between two slots names neither, so the fraction is dropped toward zero.
        [Test]
        public void IndexToValue_FractionalDouble_DropsTheFraction() =>
            Assert.AreEqual(
                "b",
                ((IConverter<double, string>)new IndexToValueConverter<string>(new[] { "a", "b", "c" })).Convert(1.9d));

        [Test]
        public void IndexToValue_NaN_IsReportedAndTakesTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("IndexToValueConverter.*an index"));

            Assert.AreEqual(
                "none",
                ((IConverter<double, string>)new IndexToValueConverter<string>(
                    new[] { "a", "b" }, IndexMode.Fallback, "none")).Convert(double.NaN));
        }

    }
}
