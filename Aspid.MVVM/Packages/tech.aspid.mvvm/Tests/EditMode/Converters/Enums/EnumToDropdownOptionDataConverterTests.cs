using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="EnumToDropdownOptionDataConverter"/> — the option built per member,
    /// authored labels, the cached list, and the guards against a misspelled or duplicate entry.
    /// </summary>
    [TestFixture]
    public sealed class EnumToDropdownOptionDataConverterTests
    {
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
                    new EnumToDropdownOptionDataConverter.Entry("Easy", "Casual"),
                });

            var options = new List<TMPro.TMP_Dropdown.OptionData>(converter.Convert(Difficulty2.Easy));

            Assert.AreEqual("Casual", options[0].text);
        }

        // An entry naming a member the enum does not declare is authored in and never reached: the
        // dropdown comes out without the label, and nothing in the Inspector says why. The option
        // list is cached per type, but the report is not: a designer opening the scene after the
        // list was built would otherwise never see it.
        [Test]
        public void EnumToDropdownOptions_EntryNamingNoMember_IsReportedEveryTime()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(
                    LogType.Error,
                    new Regex("EnumToDropdownOptionDataConverter.*not a member of Difficulty2"));

            var converter = new EnumToDropdownOptionDataConverter(
                new[]
                {
                    new EnumToDropdownOptionDataConverter.Entry("Simple", "Casual"),
                });

            var options = new List<TMPro.TMP_Dropdown.OptionData>(converter.Convert(Difficulty2.Easy));

            converter.Convert(Difficulty2.Easy);
            converter.Convert(Difficulty2.Brutal);

            Assert.AreEqual("Easy", options[0].text);
        }

        // The scan answers with the first entry that names the member, so the second one is authored
        // in and unreachable.
        [Test]
        public void EnumToDropdownOptions_DuplicateEntry_TakesTheFirstAndReportsTheSecond()
        {
            LogAssert.Expect(
                LogType.Error,
                new Regex("EnumToDropdownOptionDataConverter.*listed more than once"));

            var converter = new EnumToDropdownOptionDataConverter(
                new[]
                {
                    new EnumToDropdownOptionDataConverter.Entry("Easy", "First"),
                    new EnumToDropdownOptionDataConverter.Entry("Easy", "Second"),
                });

            var options = new List<TMPro.TMP_Dropdown.OptionData>(converter.Convert(Difficulty2.Easy));

            Assert.AreEqual("First", options[0].text);
        }

        // The option set depends on the type, not the value, so rebuilding per push would allocate
        // an OptionData per member on every notification.
        [Test]
        public void EnumToDropdownOptions_ReusesTheListWhileTheTypeIsUnchanged()
        {
            var converter = new EnumToDropdownOptionDataConverter();

            Assert.AreSame(converter.Convert(Difficulty2.Easy), converter.Convert(Difficulty2.Brutal));
        }
    }
}
