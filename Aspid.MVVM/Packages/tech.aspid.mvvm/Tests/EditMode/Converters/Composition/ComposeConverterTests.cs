#nullable enable
using System;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using static Aspid.MVVM.Tests.ConverterReflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ComposeConverter{TIn,TMiddle,TOut}"/> — applying and undoing both
    /// links, and the missing-link fallback.
    /// </summary>
    [TestFixture]
    public sealed class ComposeConverterTests
    {
        [Test]
        public void Compose_AppliesBothLinksInOrder() =>
            Assert.AreEqual(
                "8",
                new ComposeConverter<int, int, string>(new AddConverter(1), new ToText()).Convert(7));

        [Test]
        public void Compose_MissingLink_ReturnsTheFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(LogType.Error, new Regex("both links are required"));

            // The Inspector shape: a wrapper deserialized before its links are filled in.
            var converter = Empty<ComposeConverter<int, int, string>>();
            Assert.IsNull(converter.Convert(7));
            converter.Convert(8);
            converter.Convert(9);
        }

        [Test]
        public void Compose_NullLinkInTheConstructor_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new ComposeConverter<int, int, string>(new AddConverter(1), null!));
            Assert.Throws<ArgumentNullException>(() => _ = new ComposeConverter<int, int, string>(null!, new ToText()));
        }

        [Test]
        public void Compose_UndoesBothLinksInReverseOrder()
        {
            var converter = new ComposeConverter<int, int, string>(new TwoWayAddConverter(1), new TwoWayTextConverter());

            Assert.AreEqual("8", converter.Convert(7));
            Assert.AreEqual(7, converter.ConvertBack("8"));
        }

        // Undoing one link and not the other would leave the value in neither space, so a single
        // one-way link makes the whole composition one-way — and says so.
        [Test]
        public void Compose_ConvertBack_WithAOneWayLink_ReturnsTheFallbackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("ToText converts one way only"));

            var converter = new ComposeConverter<int, int, string>(new TwoWayAddConverter(1), new ToText());
            Assert.AreEqual(0, converter.ConvertBack("8"));
        }

        [Test]
        public void Compose_ConvertBack_MissingLink_ReturnsTheFallbackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("both links are required"));

            Assert.AreEqual(0, Empty<ComposeConverter<int, int, string>>().ConvertBack("8"));
        }

        private sealed class ToText : IConverter<int, string>
        {
            public string Convert(int value) => value.ToString();
        }
    }
}
