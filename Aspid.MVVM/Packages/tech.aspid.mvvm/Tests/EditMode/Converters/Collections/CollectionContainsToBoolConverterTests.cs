#nullable enable
using System;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionContainsToBoolConverter{T}"/> — matching by value or by a
    /// configured match converter, and the missing-match report.
    /// </summary>
    [TestFixture]
    public sealed class CollectionContainsToBoolConverterTests
    {
        private static readonly string[] _three = { "a", "b", "c" };

        [Test]
        public void Contains_LooksForTheItem()
        {
            Assert.IsTrue(new CollectionContainsToBoolConverter<string>("b").Convert(_three));
            Assert.IsFalse(new CollectionContainsToBoolConverter<string>("z").Convert(_three));
            Assert.IsFalse(new CollectionContainsToBoolConverter<string>("b").Convert(null));
        }

        // The match slot takes any converter, so "contains" is not limited to equality with one value.
        [Test]
        public void Contains_MatchConverter_DecidesTheMatch()
        {
            var anyEmpty = new CollectionContainsToBoolConverter<string>(new StringEmptyToBoolConverter());

            Assert.IsFalse(anyEmpty.Convert(_three));
            Assert.IsTrue(anyEmpty.Convert(new[] { "a", "" }));
        }

        [Test]
        public void Contains_NullMatchInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() =>
                _ = new CollectionContainsToBoolConverter<string>(match: null!));

        // The Inspector shape: a converter deserialized with its match slot cleared.
        [Test]
        public void Contains_MissingMatch_CountsAsNoMatchAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("the match converter is required"));

            var converter = new CollectionContainsToBoolConverter<string>("b");
            typeof(CollectionContainsToBoolConverter<string>)
                .GetField("_match", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .SetValue(converter, null);

            Assert.IsFalse(converter.Convert(_three));
        }
    }
}
