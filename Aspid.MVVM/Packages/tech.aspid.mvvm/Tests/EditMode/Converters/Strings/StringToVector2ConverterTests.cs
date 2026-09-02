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
    /// Coverage for <see cref="StringToVector2Converter"/> — the separator, the culture collision
    /// with a comma decimal point, and the component-count refusals.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class StringToVector2ConverterTests
    {
        // The collision the two halves have to agree about: a German device writes one and a half
        // as "1,5", and the separator between the components is a comma too. Written with the
        // device culture the pair would come out "1,5,2,5", which no split recovers — both halves
        // step back to the invariant reading so that what ConvertBack writes, Convert reads.
        [Test]
        [SetCulture("de-DE")]
        public void Convert_RoundTripsWhenTheCultureCollidesWithTheSeparator()
        {
            var converter = new StringToVector2Converter(",", default, CultureInfoMode.CurrentCulture);

            var text = converter.ConvertBack(new Vector2(1.5f, 2.5f));

            Assert.AreEqual("1.5,2.5", text);
            Assert.AreEqual(new Vector2(1.5f, 2.5f), converter.Convert(text));
        }

        // The other side of the same branch: with a separator the culture's decimal separator does
        // not collide with, the chosen culture is kept rather than always forced to invariant.
        [Test]
        [SetCulture("de-DE")]
        public void Convert_NonCollidingSeparator_KeepsTheChosenCulture()
        {
            var converter = new StringToVector2Converter("; ", default, CultureInfoMode.CurrentCulture);

            var text = converter.ConvertBack(new Vector2(1.5f, 2.5f));

            Assert.AreEqual("1,5; 2,5", text);
            Assert.AreEqual(new Vector2(1.5f, 2.5f), converter.Convert(text));
        }

        // Text copied out of a console or a log arrives wrapped, with a space after the comma.
        [Test]
        public void Convert_ReadsWhatVectorToStringWrites() =>
            Assert.AreEqual(new Vector2(1f, 2f), new StringToVector2Converter().Convert("(1.00, 2.00)"));

        // The Inspector can clear the separator field, and the stand-in has to be the same on both
        // halves: a write that joined with nothing would put "12" on screen for (1, 2).
        [Test]
        public void Convert_EmptySeparator_StandsInAComma()
        {
            var converter = new StringToVector2Converter(string.Empty);

            var text = converter.ConvertBack(new Vector2(1f, 2f));

            Assert.AreEqual("1,2", text);
            Assert.AreEqual(new Vector2(1f, 2f), converter.Convert(text));
        }

        // Thousands are refused inside a component: the group separator and the separator between
        // components are the same character in most cultures, so accepting both would make "1,5"
        // a vector in one reading and fifteen thousand in the other.
        [Test]
        public void Convert_GroupedComponent_IsRefused()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToVector2Converter"));

            Assert.AreEqual(Vector2.zero, new StringToVector2Converter(";").Convert("1,000;2"));
        }

        // Three numbers are not a Vector2: the tail is read as one component and refused, rather
        // than the extra being dropped and a wrong-but-plausible vector pushed on.
        [Test]
        public void Convert_ExtraComponent_IsRefused()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToVector2Converter"));

            Assert.AreEqual(Vector2.zero, new StringToVector2Converter().Convert("1,2,3"));
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void Convert_BlankText_TakesTheFallbackQuietly(string value)
        {
            Assert.AreEqual(
                new Vector2(9f, 9f),
                new StringToVector2Converter(",", new Vector2(9f, 9f)).Convert(value));
            LogAssert.NoUnexpectedReceived();
        }

        // The last reading is cached because splitting allocates on every push. The separator is
        // part of the key: it is editable while the game runs, and a hit that ignored it would
        // freeze the old reading in.
        [Test]
        public void Convert_CachedReading_IsDroppedWhenTheSeparatorChanges()
        {
            var converter = new StringToVector2Converter();
            Assert.AreEqual(new Vector2(1f, 2f), converter.Convert("1,2"));

            With(converter, "_separator", ";");
            LogAssert.Expect(LogType.Error, new Regex("StringToVector2Converter"));

            Assert.AreEqual(Vector2.zero, converter.Convert("1,2"));
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
