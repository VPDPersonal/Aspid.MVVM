using TMPro;
using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the dropdown options binders.
    /// </summary>
    /// <remarks>
    /// Two defects, both from mutating <c>TMP_Dropdown.options</c> by hand. TMP refreshes the caption inside
    /// <c>AddOptions</c> and <c>ClearOptions</c>; the <c>IEnumerable&lt;OptionData&gt;</c> overloads bypassed both and
    /// left the caption showing the previous set — empty for the MonoBinder, stale text for the serializable twin.
    /// Separately, <c>ClearOptions</c> resets the selected index and raises nothing, so a ViewModel holding the old
    /// index quietly disagreed with the control after every options update.
    /// </remarks>
    [TestFixture]
    public sealed class DropdownOptionsTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void MonoBinder_OptionDataOverload_RefreshesTheCaption()
        {
            var (dropdown, binder) = NewMonoBinder();

            binder.SetValue(new[]
            {
                new TMP_Dropdown.OptionData("first"),
                new TMP_Dropdown.OptionData("second"),
            });

            Assert.AreEqual(2, dropdown.options.Count);
            Assert.AreEqual("first", Caption(dropdown), "Подпись не обновилась после смены набора опций");
        }

        [Test]
        public void MonoBinder_KeepsTheSelectionWhenTheNewListIsLongEnough()
        {
            var (dropdown, binder) = NewMonoBinder();

            binder.SetValue(new List<string> { "a", "b", "c" });
            dropdown.SetValueWithoutNotify(2);

            binder.SetValue(new List<string> { "x", "y", "z" });

            Assert.AreEqual(2, dropdown.value, "Выбранный индекс сбросился при обновлении опций");
            Assert.AreEqual("z", Caption(dropdown));
        }

        /// <summary>
        /// Guards the fix rather than the defect: it passes either way, because TMP already resets the index to 0
        /// inside <c>ClearOptions</c> and that happens to equal the clamped result here. Kept to pin that restoring
        /// the selection never puts it out of range.
        /// </summary>
        [Test]
        public void MonoBinder_ClampsTheSelectionWhenTheNewListIsShorter()
        {
            var (dropdown, binder) = NewMonoBinder();

            binder.SetValue(new List<string> { "a", "b", "c" });
            dropdown.SetValueWithoutNotify(2);

            binder.SetValue(new List<string> { "x" });

            Assert.AreEqual(0, dropdown.value);
            Assert.AreEqual("x", Caption(dropdown));
        }

        [Test]
        public void SerializableBinder_OptionDataOverload_RefreshesTheCaption()
        {
            var dropdown = NewDropdown();
            var binder = new DropdownOptionsBinder(dropdown);

            binder.SetValue(new List<string> { "old" });
            binder.SetValue(new[] { new TMP_Dropdown.OptionData("new") });

            Assert.AreEqual(1, dropdown.options.Count);
            Assert.AreEqual("new", Caption(dropdown), "Подпись осталась от прежнего набора опций");
        }

        private static string Caption(TMP_Dropdown dropdown) =>
            dropdown.captionText.text;

        private (TMP_Dropdown dropdown, DropdownOptionsMonoBinder binder) NewMonoBinder()
        {
            var dropdown = NewDropdown();
            return (dropdown, dropdown.gameObject.AddComponent<DropdownOptionsMonoBinder>());
        }

        /// <summary>
        /// A dropdown with a caption label attached, since the caption is what the refresh defect shows up in.
        /// </summary>
        private TMP_Dropdown NewDropdown()
        {
            var gameObject = new GameObject("Dropdown");
            _spawned.Add(gameObject);

            var dropdown = gameObject.AddComponent<TMP_Dropdown>();
            var label = new GameObject("Label").AddComponent<TextMeshProUGUI>();

            label.transform.SetParent(gameObject.transform, worldPositionStays: false);
            dropdown.captionText = label;

            return dropdown;
        }
    }
}
