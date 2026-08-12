#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEditor;
using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for two-way binding on <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    /// <remarks>
    /// A dropdown exists to be chosen from, and nothing carried that choice back: the binder could set the
    /// selection but never learn of one. These pin the three things that makes true — the user's choice reaches
    /// the ViewModel, the binder's own write does not come back as a choice, and a refused index is corrected
    /// rather than left believed in.
    /// </remarks>
    [TestFixture]
    public sealed class DropdownTwoWayTests
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
        public void AUserChoice_ReachesTheViewModel()
        {
            var (dropdown, binder) = NewDropdown(BindMode.TwoWay);
            var received = new List<int>();

            ((IReverseBinder<int>)binder).ValueChanged += received.Add;
            binder.Bind(new TwoWayStructBindableMember<int>(0, _ => { }));

            dropdown.value = 2;

            Assert.Contains(2, received, "Выбор пользователя не дошёл до ViewModel");
        }

        /// <summary>
        /// Assigning <see cref="TMP_Dropdown.value"/> raises its change event exactly as a click does, so a
        /// binder that wrote through the property would read its own write back as a user choice.
        /// </summary>
        [Test]
        public void TheBindersOwnWrite_DoesNotComeBackAsAChoice()
        {
            var (dropdown, binder) = NewDropdown(BindMode.TwoWay);

            binder.Bind(new TwoWayStructBindableMember<int>(0, _ => { }));

            var received = new List<int>();
            ((IReverseBinder<int>)binder).ValueChanged += received.Add;

            binder.SetValue(1);

            Assert.AreEqual(1, dropdown.value, "Значение не доехало до дропдауна");
            Assert.IsEmpty(received, "Собственная запись биндера вернулась как выбор пользователя");
        }

        /// <summary>
        /// Unity refuses an index outside the options that exist. The ViewModel is told what the dropdown
        /// actually holds rather than being left believing in the index it sent.
        /// </summary>
        [Test]
        public void ARefusedIndex_IsReportedBack()
        {
            var (dropdown, binder) = NewDropdown(BindMode.TwoWay);

            binder.Bind(new TwoWayStructBindableMember<int>(0, _ => { }));

            var received = new List<int>();
            ((IReverseBinder<int>)binder).ValueChanged += received.Add;

            binder.SetValue(99);

            Assert.IsNotEmpty(received, "ViewModel не узнала, что индекс был отвергнут");
            Assert.AreEqual(dropdown.value, received[^1], "ViewModel сообщили не то значение, что в дропдауне");
        }

        [Test]
        public void InOneWay_TheBinderDoesNotListenToTheDropdown()
        {
            var (dropdown, binder) = NewDropdown(BindMode.OneWay);
            var received = new List<int>();

            ((IReverseBinder<int>)binder).ValueChanged += received.Add;
            binder.Bind(new TwoWayStructBindableMember<int>(0, _ => { }));

            dropdown.value = 2;

            Assert.IsEmpty(received, "В OneWay биндер всё равно слушает дропдаун");
        }

        private (TMP_Dropdown Dropdown, DropdownValueMonoBinder Binder) NewDropdown(BindMode mode)
        {
            var gameObject = new GameObject("Dropdown");
            _spawned.Add(gameObject);

            var dropdown = gameObject.AddComponent<TMP_Dropdown>();
            dropdown.options = new List<TMP_Dropdown.OptionData>
            {
                new("Один"),
                new("Два"),
                new("Три"),
            };

            var binder = gameObject.AddComponent<DropdownValueMonoBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return (dropdown, binder);
        }
    }
}
#endif
