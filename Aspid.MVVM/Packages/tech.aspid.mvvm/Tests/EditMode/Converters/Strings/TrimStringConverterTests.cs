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
    /// Coverage for <see cref="TrimStringConverter"/> — the three <see cref="TrimSide"/> options and
    /// re-authoring the trimmed characters at runtime.
    /// </summary>
    [TestFixture]
    public sealed class TrimStringConverterTests
    {
        [TestCase(TrimSide.Both, "  abc  ", "abc")]
        [TestCase(TrimSide.Start, "  abc  ", "abc  ")]
        [TestCase(TrimSide.End, "  abc  ", "  abc")]
        public void Convert_TrimsTheRequestedEnds(TrimSide side, string value, string expected) =>
            Assert.AreEqual(expected, new TrimStringConverter(side).Convert(value));

        [Test]
        public void Convert_TakesSpecificCharacters() =>
            Assert.AreEqual("abc", new TrimStringConverter(TrimSide.Both, "*").Convert("**abc**"));

        // Every declared side removes at least one of the two runs of stars, so the untouched string
        // is what tells an undeclared side apart from a trim that simply found nothing to take.
        [Test]
        public void Convert_UndeclaredSide_ReportsAndReturnsTheStringUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("TrimStringConverter.*not a declared TrimSide"));

            Assert.AreEqual("**abc**", new TrimStringConverter((TrimSide)42, "*").Convert("**abc**"));
        }

        // The characters are made once and kept, so re-authoring the field has to reach them. Unity
        // reads the object again after an Inspector edit, which is what SetField imitates.
        [Test]
        public void Convert_CharactersReauthored_TrimsTheNewOnes()
        {
            var converter = new TrimStringConverter(TrimSide.Both, "*");
            Assert.AreEqual("abc", converter.Convert("**abc**"));

            SetField(converter, "_trimChars", "#");

            Assert.AreEqual("abc", converter.Convert("##abc##"));
            Assert.AreEqual("**abc**", converter.Convert("**abc**"));
        }

        // An empty field means whitespace, and the emptiness has to survive the same round trip.
        [Test]
        public void Convert_CharactersClearedToEmpty_GoesBackToWhitespace()
        {
            var converter = new TrimStringConverter(TrimSide.Both, "*");
            Assert.AreEqual("abc", converter.Convert("**abc**"));

            SetField(converter, "_trimChars", string.Empty);

            Assert.AreEqual("abc", converter.Convert("  abc  "));
        }

        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field.SetValue(target, value);

            if (target is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();
        }
    }
}
