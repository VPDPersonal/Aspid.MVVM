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
    /// Coverage for <see cref="InverseConverter{TIn,TOut}"/> — running the wrapped two-way converter
    /// backwards, and the missing-converter fallback.
    /// </summary>
    [TestFixture]
    public sealed class InverseConverterTests
    {
        [Test]
        public void Inverse_RunsTheWrappedConverterTheOtherWayRound()
        {
            var converter = new InverseConverter<int, string>(new TwoWayTextConverter());

            Assert.AreEqual(7, converter.Convert("7"));
            Assert.AreEqual("7", converter.ConvertBack(7));
        }

        [Test]
        public void Inverse_NullInnerInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new InverseConverter<int, string>(null!));

        [Test]
        public void Inverse_MissingConverter_ReturnsTheDefaultAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("the converter is required"));

            var converter = Empty<InverseConverter<int, string>>();
            Assert.AreEqual(0, converter.Convert("7"));
            converter.Convert("8");
        }
    }
}
