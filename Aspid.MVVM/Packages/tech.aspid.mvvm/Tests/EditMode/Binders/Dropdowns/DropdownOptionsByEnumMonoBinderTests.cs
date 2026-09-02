using TMPro;
using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal enum Difficulty
    {
        Easy,
        Normal,
        Hard,
    }

    internal enum Season
    {
        Winter,
        Spring,
    }

    /// <summary>
    /// Coverage for <see cref="DropdownOptionsByEnumMonoBinder"/>: the option set depends on the enum
    /// type, so pushing the same type again must not rebuild the list — and rebuilding must not throw
    /// the current selection away.
    /// </summary>
    [TestFixture]
    public sealed class DropdownOptionsByEnumMonoBinderTests : SceneFixture
    {
        private TMP_Dropdown _dropdown;
        private DropdownOptionsByEnumMonoBinder _binder;

        [SetUp]
        public void CreateDropdown()
        {
            var gameObject = Spawn(nameof(DropdownOptionsByEnumMonoBinderTests));

            _dropdown = gameObject.AddComponent<TMP_Dropdown>();
            _binder = gameObject.AddComponent<DropdownOptionsByEnumMonoBinder>();
        }

        [Test]
        public void SetValue_PopulatesOneOptionPerEnumMember()
        {
            _binder.SetValue(Difficulty.Easy);

            Assert.AreEqual(3, _dropdown.options.Count);
            Assert.AreEqual("Easy", _dropdown.options[0].text);
            Assert.AreEqual("Normal", _dropdown.options[1].text);
            Assert.AreEqual("Hard", _dropdown.options[2].text);
        }

        // The selection lives on the same dropdown and is usually driven by a second binder, so a
        // push that changes nothing must not reset it.
        [Test]
        public void SetValue_SameEnumType_KeepsTheSelection()
        {
            _binder.SetValue(Difficulty.Easy);
            _dropdown.SetValueWithoutNotify(2);

            _binder.SetValue(Difficulty.Hard);

            Assert.AreEqual(2, _dropdown.value);
        }

        [Test]
        public void SetValue_DifferentEnumType_RebuildsTheOptions()
        {
            _binder.SetValue(Difficulty.Easy);
            _binder.SetValue(Season.Winter);

            Assert.AreEqual(2, _dropdown.options.Count);
            Assert.AreEqual("Winter", _dropdown.options[0].text);
        }

        [Test]
        public void SetValue_RebuildClampsASelectionThatNoLongerExists()
        {
            _binder.SetValue(Difficulty.Easy);
            _dropdown.SetValueWithoutNotify(2);

            _binder.SetValue(Season.Winter);

            Assert.AreEqual(1, _dropdown.value);
        }

        [Test]
        public void SetValue_Null_IsIgnored()
        {
            _binder.SetValue(null);

            Assert.AreEqual(0, _dropdown.options.Count);
        }
    }
}
