using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aspid.FastTools.Types.Editors.Tests
{
    /// <summary>
    /// Coverage for <see cref="TypeUtility"/> — the name formatting every type-selector surface
    /// (rows, captions, tooltips, error messages) builds on.
    /// </summary>
    [TestFixture]
    internal sealed class TypeUtilityTests
    {
        [TestCase("List`1", "List")]
        [TestCase("Dictionary`2", "Dictionary")]
        [TestCase("PlainName", "PlainName")]
        [TestCase("", "")]
        public void StripArity_RemovesTheBacktickSuffix(string rawName, string expected) =>
            Assert.AreEqual(expected, TypeUtility.StripArity(rawName));

        [Test]
        public void FormatGenericName_NonGeneric_ReturnsTheShortName() =>
            Assert.AreEqual("Int32", TypeUtility.FormatGenericName(typeof(int)));

        [Test]
        public void FormatGenericName_ClosedGeneric_SpellsTheArguments() =>
            Assert.AreEqual("List<Int32>", TypeUtility.FormatGenericName(typeof(List<int>)));

        [Test]
        public void FormatGenericName_MultipleArguments_AreCommaSeparated() =>
            Assert.AreEqual("Dictionary<Int32, String>", TypeUtility.FormatGenericName(typeof(Dictionary<int, string>)));

        [Test]
        public void FormatGenericName_NestedGeneric_RecursesIntoTheArguments() =>
            Assert.AreEqual("List<List<Int32>>", TypeUtility.FormatGenericName(typeof(List<List<int>>)));

        [Test]
        public void FormatGenericName_OpenDefinition_KeepsTheParameterName() =>
            Assert.AreEqual("List<T>", TypeUtility.FormatGenericName(typeof(List<>)));

        [Test]
        public void EnumerateDomainTypes_YieldsTypesFromTheLoadedAssemblies()
        {
            var found = false;

            foreach (var type in TypeUtility.EnumerateDomainTypes())
            {
                Assert.IsNotNull(type, "Unloadable entries must be filtered out, never yielded as null.");

                if (type != typeof(TypeUtilityTests)) continue;
                found = true;
                break;
            }

            Assert.IsTrue(found, "A type of this very test assembly must be part of the domain sweep.");
        }
    }
}
