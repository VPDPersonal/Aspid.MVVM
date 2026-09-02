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
    /// Coverage for <see cref="StringToLongConverter"/> — reading, the reported failure and the
    /// authored clamp bounds.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class StringToLongConverterTests
    {
        [Test]
        public void Convert_Reads() =>
            Assert.AreEqual(9_000_000_000L, new StringToLongConverter().Convert("9000000000"));

        [Test]
        public void Convert_UnreadableTextFallsBackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToLongConverter"));
            Assert.AreEqual(-1L, new StringToLongConverter(-1L).Convert("nonsense"));
        }

        [Test]
        public void Convert_RoundTrips()
        {
            var converter = new StringToLongConverter();

            Assert.AreEqual(9_000_000_000L, converter.Convert(converter.ConvertBack(9_000_000_000L)));
        }

        [TestCase("42", 10L)]
        [TestCase("-1", 0L)]
        [TestCase("5", 5L)]
        public void Convert_Clamp_HoldsTheResultInsideTheBounds(string value, long expected) =>
            Assert.AreEqual(expected, Clamped(new StringToLongConverter(), 0L, 10L).Convert(value));

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
