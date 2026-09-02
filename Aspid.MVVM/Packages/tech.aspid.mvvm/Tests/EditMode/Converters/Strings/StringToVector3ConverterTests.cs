using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToVector3Converter"/> — the culture collision with a comma
    /// decimal point and the component-count refusal.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class StringToVector3ConverterTests
    {
        // See StringToVector2ConverterTests for the collision both converters step back from.
        [Test]
        [SetCulture("de-DE")]
        public void Convert_RoundTripsWhenTheCultureCollidesWithTheSeparator()
        {
            var converter = new StringToVector3Converter(",", default, CultureInfoMode.CurrentCulture);

            var text = converter.ConvertBack(new Vector3(1.5f, 2.5f, 3.5f));

            Assert.AreEqual("1.5,2.5,3.5", text);
            Assert.AreEqual(new Vector3(1.5f, 2.5f, 3.5f), converter.Convert(text));
        }

        [Test]
        public void Convert_ReadsWhatVectorToStringWrites() =>
            Assert.AreEqual(new Vector3(1f, 2f, 3f), new StringToVector3Converter().Convert("(1.00, 2.00, 3.00)"));

        [Test]
        public void Convert_MissingComponent_IsRefused()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToVector3Converter"));

            Assert.AreEqual(Vector3.zero, new StringToVector3Converter().Convert("1,2"));
        }
    }
}
