using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the collection converters and the dropdown-options converter that closes the last
    /// empty picker.
    /// </summary>
    [TestFixture]
    internal sealed class CollectionConverterTests
    {
        private static readonly string[] Three = { "a", "b", "c" };

        [Test]
        public void Count_CountsTheItems() =>
            Assert.AreEqual(3, new CollectionCountConverter<string>().Convert(Three));

        [Test]
        public void Count_NullIsZero() =>
            Assert.AreEqual(0, new CollectionCountConverter<string>().Convert(null));

        [Test]
        public void EmptyToBool_ReportsEmptiness()
        {
            Assert.IsTrue(new CollectionEmptyToBoolConverter<string>().Convert(Array.Empty<string>()));
            Assert.IsTrue(new CollectionEmptyToBoolConverter<string>().Convert(null));
            Assert.IsFalse(new CollectionEmptyToBoolConverter<string>().Convert(Three));
        }

        [Test]
        public void ListToString_JoinsWithTheSeparator() =>
            Assert.AreEqual("a, b, c", new ListToStringConverter<string>(", ").Convert(Three));

        [Test]
        public void ListToString_TrimsAndReportsTheOverflow() =>
            Assert.AreEqual("a, b +1 more", new ListToStringConverter<string>(", ", maxItems: 2).Convert(Three));

        [Test]
        public void ListToString_EmptyUsesTheEmptyText() =>
            Assert.AreEqual("—", new ListToStringConverter<string>(", ", 0, "—").Convert(Array.Empty<string>()));

        [Test]
        public void ListToString_NullUsesTheEmptyText() =>
            Assert.AreEqual("—", new ListToStringConverter<string>(", ", 0, "—").Convert(null));

        // The builder is reused between calls, so a second call must not see the first one's text.
        [Test]
        public void ListToString_ReusedBuilderDoesNotLeakBetweenCalls()
        {
            var converter = new ListToStringConverter<string>(", ");

            Assert.AreEqual("a, b, c", converter.Convert(Three));
            Assert.AreEqual("x", converter.Convert(new[] { "x" }));
        }

        [Test]
        public void ElementAt_TakesTheIndex() =>
            Assert.AreEqual("b", new CollectionElementAtConverter<string>(1).Convert(Three));

        [Test]
        public void ElementAt_CountsFromTheEndWhenAsked() =>
            Assert.AreEqual("c", new CollectionElementAtConverter<string>(0, fromEnd: true).Convert(Three));

        [Test]
        public void ElementAt_OutsideTheListGivesTheFallback() =>
            Assert.AreEqual("?", new CollectionElementAtConverter<string>(9, false, "?").Convert(Three));

        [Test]
        public void Contains_LooksForTheItem()
        {
            Assert.IsTrue(new CollectionContainsToBoolConverter<string>("b").Convert(Three));
            Assert.IsFalse(new CollectionContainsToBoolConverter<string>("z").Convert(Three));
            Assert.IsFalse(new CollectionContainsToBoolConverter<string>("b").Convert(null));
        }

        [TestCase(Aggregate.Sum, 6f)]
        [TestCase(Aggregate.Average, 2f)]
        [TestCase(Aggregate.Min, 1f)]
        [TestCase(Aggregate.Max, 3f)]
        public void Aggregate_Reduces(Aggregate operation, float expected) =>
            Assert.AreEqual(
                expected,
                new CollectionAggregateConverter(operation).Convert(new[] { 1f, 2f, 3f }),
                1e-5f);

        [Test]
        public void Aggregate_EmptyUsesTheEmptyResult() =>
            Assert.AreEqual(
                -1f,
                new CollectionAggregateConverter(Aggregate.Min, -1f).Convert(Array.Empty<float>()),
                1e-5f);

        [Test]
        public void EnumToDropdownOptions_BuildsOnePerMember()
        {
            var options = new List<TMPro.TMP_Dropdown.OptionData>(
                new EnumToDropdownOptionDataConverter().Convert(Difficulty2.Easy));

            Assert.AreEqual(2, options.Count);
            Assert.AreEqual("Easy", options[0].text);
        }

        [Test]
        public void EnumToDropdownOptions_UsesTheInspectorName()
        {
            var options = new List<TMPro.TMP_Dropdown.OptionData>(
                new EnumToDropdownOptionDataConverter().Convert(Difficulty2.Easy));

            Assert.AreEqual("Very hard", options[1].text);
        }

        [Test]
        public void EnumToDropdownOptions_AuthoredLabelWins()
        {
            var converter = new EnumToDropdownOptionDataConverter(
                new[]
                {
                    new EnumToDropdownOptionDataConverter.Entry { Name = "Easy", Label = "Casual" },
                });

            var options = new List<TMPro.TMP_Dropdown.OptionData>(converter.Convert(Difficulty2.Easy));

            Assert.AreEqual("Casual", options[0].text);
        }

        // The option set depends on the type, not the value, so rebuilding per push would allocate
        // an OptionData per member on every notification.
        [Test]
        public void EnumToDropdownOptions_ReusesTheListWhileTheTypeIsUnchanged()
        {
            var converter = new EnumToDropdownOptionDataConverter();

            Assert.AreSame(converter.Convert(Difficulty2.Easy), converter.Convert(Difficulty2.Brutal));
        }

        [Test]
        public void MaterialInstance_ReturnsACopyAndReusesIt()
        {
            var shader = Shader.Find("Unlit/Color") ?? Shader.Find("Sprites/Default");
            var material = new Material(shader) { name = "Shared" };
            var converter = new MaterialInstanceConverter();

            try
            {
                var first = converter.Convert(material);

                Assert.AreNotSame(material, first);
                Assert.AreSame(first, converter.Convert(material));
                Assert.IsTrue(first!.name.Contains("Instance"));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(material);
            }
        }

        [Test]
        public void MaterialInstance_CanBeTurnedOff()
        {
            var shader = Shader.Find("Unlit/Color") ?? Shader.Find("Sprites/Default");
            var material = new Material(shader);

            try
            {
                Assert.AreSame(material, new MaterialInstanceConverter(instantiate: false).Convert(material));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(material);
            }
        }

        [Test]
        public void MaterialInstance_NullPassesThrough() =>
            Assert.IsNull(new MaterialInstanceConverter().Convert(null));
    }
}
