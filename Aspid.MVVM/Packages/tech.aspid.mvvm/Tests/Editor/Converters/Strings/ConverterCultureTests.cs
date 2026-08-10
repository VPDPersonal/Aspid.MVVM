using System;
using System.Threading;
using NUnit.Framework;
using System.Reflection;
using System.Globalization;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the culture a string converter formats with.
    /// </summary>
    /// <remarks>
    /// Every assertion sets the thread culture to German first, because the bug this closes is
    /// invisible on an English machine: a converter formatted under the device locale with no way to
    /// override it, so <c>{0:F2}</c> produced <c>3,14</c> on a German device and nothing in the
    /// Inspector could say otherwise.
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterCultureTests
    {
        private CultureInfo _previous;

        [SetUp]
        public void UseAGermanDevice()
        {
            _previous = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");
        }

        [TearDown]
        public void RestoreCulture() =>
            Thread.CurrentThread.CurrentCulture = _previous;

        [Test]
        public void Format_DefaultsToTheDeviceCulture() =>
            Assert.AreEqual("3,14", new GenericToStringConverter<float>("{0:F2}").Convert(3.14159f));

        [Test]
        public void Format_HonoursTheConfiguredCulture() =>
            Assert.AreEqual(
                "3.14",
                WithCulture(new GenericToStringConverter<float>("{0:F2}"), CultureInfoMode.InvariantCulture).Convert(3.14159f));

        [Test]
        public void NoFormat_DefaultsToTheDeviceCulture() =>
            Assert.AreEqual("3,14", new GenericToStringConverter<float>().Convert(3.14f));

        [Test]
        public void NoFormat_HonoursTheConfiguredCulture() =>
            Assert.AreEqual(
                "3.14",
                WithCulture(new GenericToStringConverter<float>(), CultureInfoMode.InvariantCulture).Convert(3.14f));

        [Test]
        public void StringFormatConverter_InheritsTheCultureField() =>
            Assert.AreEqual(
                "HP: abc",
                WithCulture(new StringFormatConverter("HP: {0}"), CultureInfoMode.InvariantCulture).Convert("abc"));

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
        public void AnUndeclaredModeThrowsWithItsValue() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => ((CultureInfoMode)99).ToCultureInfo());

        // The culture is Inspector state with no constructor overload, so a test sets it the way the
        // Inspector does. Walking the base chain covers StringFormatConverter, which inherits it.
        private static T WithCulture<T>(T converter, CultureInfoMode mode)
            where T : class
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

            for (var type = converter.GetType(); type is not null; type = type.BaseType)
            {
                var field = type.GetField("_culture", flags);
                if (field is null) continue;

                field.SetValue(converter, mode);
                return converter;
            }

            throw new InvalidOperationException($"{converter.GetType().Name} has no _culture field.");
        }
    }
}
