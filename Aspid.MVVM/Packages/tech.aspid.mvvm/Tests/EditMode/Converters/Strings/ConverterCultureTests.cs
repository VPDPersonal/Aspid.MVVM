using System;
using UnityEngine;
using NUnit.Framework;
using System.Globalization;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for the culture a string converter formats with.
    /// </summary>
    /// <remarks>
    /// The fixture runs under German: a decimal-comma culture is where a converter stuck reading the
    /// device locale shows, while <c>3.14</c> and <c>3,14</c> both read as valid numbers on an
    /// English one.
    /// </remarks>
    [TestFixture]
    [SetCulture("de-DE")]
    public sealed class ConverterCultureTests
    {
        [Test]
        public void Format_DefaultsToTheDeviceCulture() =>
            Assert.AreEqual("3,14", new ValueToStringConverter<float>("{0:F2}").Convert(3.14159f));

        [Test]
        public void Format_HonoursTheConfiguredCulture() =>
            Assert.AreEqual(
                "3.14",
                new ValueToStringConverter<float>("{0:F2}", CultureInfoMode.InvariantCulture).Convert(3.14159f));

        [Test]
        public void NoFormat_DefaultsToTheDeviceCulture() =>
            Assert.AreEqual("3,14", new ValueToStringConverter<float>().Convert(3.14f));

        [Test]
        public void NoFormat_HonoursTheConfiguredCulture() =>
            Assert.AreEqual(
                "3.14",
                new ValueToStringConverter<float>(null, CultureInfoMode.InvariantCulture).Convert(3.14f));

        [Test]
        public void StringFormatConverter_InheritsTheCultureField() =>
            Assert.AreEqual(
                "HP: abc",
                new StringFormatConverter("HP: {0}", culture: CultureInfoMode.InvariantCulture).Convert("abc"));

        // Both DefaultThread statics are null until an application sets them, which is the usual
        // state — two of the six dropdown entries used to resolve to a null culture.
        [TestCase(CultureInfoMode.DefaultThreadCurrentCulture)]
        [TestCase(CultureInfoMode.DefaultThreadCurrentUICulture)]
        public void UnsetDefaultThreadCultures_FallBackInsteadOfReturningNull(CultureInfoMode mode)
        {
            var previous = CultureInfo.DefaultThreadCurrentCulture;
            var previousUi = CultureInfo.DefaultThreadCurrentUICulture;

            try
            {
                CultureInfo.DefaultThreadCurrentCulture = null;
                CultureInfo.DefaultThreadCurrentUICulture = null;

                Assert.IsNotNull(mode.ToCultureInfo());
            }
            finally
            {
                CultureInfo.DefaultThreadCurrentCulture = previous;
                CultureInfo.DefaultThreadCurrentUICulture = previousUi;
            }
        }

        [TestCase(CultureInfoMode.CurrentCulture)]
        [TestCase(CultureInfoMode.CurrentUICulture)]
        [TestCase(CultureInfoMode.InvariantCulture)]
        [TestCase(CultureInfoMode.InstalledUICulture)]
        public void EveryDeclaredModeResolves(CultureInfoMode mode) =>
            Assert.IsNotNull(mode.ToCultureInfo());

        [Test]
        public void AnUndeclaredModeReportsAndReadsAsTheCurrentCulture()
        {
            UnityEngine.TestTools.LogAssert.Expect(UnityEngine.LogType.Error,
                new System.Text.RegularExpressions.Regex("CultureInfoMode.*not a declared value"));

            Assert.AreEqual(CultureInfo.CurrentCulture, ((CultureInfoMode)99).ToCultureInfo());
        }

    }
}
