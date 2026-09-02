using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionElementAtConverter{T}"/> — the forward and from-end index,
    /// and the out-of-range fallback.
    /// </summary>
    [TestFixture]
    public sealed class CollectionElementAtConverterTests
    {
        private static readonly string[] _three = { "a", "b", "c" };

        [Test]
        public void ElementAt_TakesTheIndex() =>
            Assert.AreEqual("b", new CollectionElementAtConverter<string>(1).Convert(_three));

        [Test]
        public void ElementAt_CountsFromTheEndWhenAsked() =>
            Assert.AreEqual("c", new CollectionElementAtConverter<string>(0, fromEnd: true).Convert(_three));

        [Test]
        public void ElementAt_OutsideTheListReportsItAndGivesTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("CollectionElementAtConverter.*outside the list"));

            Assert.AreEqual("?", new CollectionElementAtConverter<string>(9, false, "?").Convert(_three));
        }

        [Test]
        public void ElementAt_NegativeIndex_IsRejectedByTheConstructor() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new CollectionElementAtConverter<string>(-1));
    }
}
