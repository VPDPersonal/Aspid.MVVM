using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToIntConverter"/> — the quiet blank fallback, the reported
    /// failure and the authored clamp bounds.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class StringToIntConverterTests
    {
        // Blank text is an unfilled field rather than a malformed number, so it takes the fallback
        // without a word; text that is present but unreadable is reported.
        [TestCase("42", 42)]
        [TestCase("-7", -7)]
        [TestCase("", 0)]
        [TestCase(null, 0)]
        public void Convert_ReadsOrFallsBackQuietly(string value, int expected) =>
            Assert.AreEqual(expected, new StringToIntConverter().Convert(value));

        [TestCase("abc", 0)]
        [TestCase("1.5", 0)]
        public void Convert_UnreadableTextFallsBackAndReports(string value, int expected)
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter"));
            Assert.AreEqual(expected, new StringToIntConverter().Convert(value));
        }

        [Test]
        public void Convert_UsesTheAuthoredFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter"));
            Assert.AreEqual(-1, new StringToIntConverter(-1).Convert("nonsense"));
        }

        [Test]
        public void Convert_RoundTrips()
        {
            var converter = new StringToIntConverter();

            Assert.AreEqual(42, converter.Convert(converter.ConvertBack(42)));
        }

        [TestCase("42", 10)]
        [TestCase("-1", 0)]
        [TestCase("5", 5)]
        public void Convert_Clamp_HoldsTheResultInsideTheBounds(string value, int expected) =>
            Assert.AreEqual(expected, Clamped(new StringToIntConverter(), 0, 10).Convert(value));

        // Every failure is reported. A value that stops converting halfway through a session is
        // the case a report-once rule hides, and a console line is cheaper than that.
        [Test]
        public void Convert_Unparsed_ReportsEveryFailure()
        {
            var converter = new StringToIntConverter(fallback: -1);

            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter.*a whole number"));
            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter.*a whole number"));

            Assert.AreEqual(-1, converter.Convert("not a number"));
            Assert.AreEqual(-1, converter.Convert("still not a number"));
        }

        // The clamp is three Inspector fields with no constructor overload.
        private static T Clamped<T>(T converter, object min, object max)
            where T : class
        {
            With(converter, "_clamp", true);
            With(converter, "_min", min);
            With(converter, "_max", max);

            return converter;
        }

        private static T With<T>(T converter, string field, object value)
            where T : class
        {
            var info = converter.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(info, $"{converter.GetType().Name} has no field {field}");

            info.SetValue(converter, value);

            if (converter is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();

            return converter;
        }
    }
}
