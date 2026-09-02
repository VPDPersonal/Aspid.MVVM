using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="PadStringConverter"/> — padding to a fixed width on either side.
    /// </summary>
    [TestFixture]
    public sealed class PadStringConverterTests
    {
        [Test]
        public void Convert_PadsToWidth()
        {
            Assert.AreEqual("     abc", new PadStringConverter(8).Convert("abc"));
            Assert.AreEqual("abc     ", new PadStringConverter(8, padLeft: false).Convert("abc"));
        }

        // PadLeft and PadRight throw on a negative width, and the field is an Inspector int with
        // nothing to stop one being typed.
        [Test]
        public void Convert_NegativeWidth_ReportsEveryPushAndLeavesTheStringAlone()
        {
            var converter = new PadStringConverter(-4);

            LogAssert.Expect(LogType.Error, new Regex("PadStringConverter.*negative"));
            LogAssert.Expect(LogType.Error, new Regex("PadStringConverter.*negative"));

            Assert.AreEqual("abc", converter.Convert("abc"));
            Assert.AreEqual("xy", converter.Convert("xy"));
            Assert.IsNull(converter.Convert(null), "a null value never reaches the width at all");
        }
    }
}
