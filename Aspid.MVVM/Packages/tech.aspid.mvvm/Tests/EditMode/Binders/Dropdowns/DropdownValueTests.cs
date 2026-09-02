#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for two-way binding on <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    [TestFixture]
    public sealed class DropdownValueTests : SceneFixture
    {
        [Test]
        public void AUserChoice_ReachesTheViewModel()
        {
            var (dropdown, binder) = NewDropdown(BindMode.TwoWay);
            var received = new List<int>();

            ((IReverseBinder<int>)binder).ValueChanged += received.Add;
            binder.Bind(new TwoWayStructBindableMember<int>(0, _ => { }));

            dropdown.value = 2;

            Assert.Contains(2, received, "The user's choice did not reach the ViewModel");
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

            Assert.AreEqual(1, dropdown.value, "The value did not reach the dropdown");
            Assert.IsEmpty(received, "The binder's own write came back as a user choice");
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

            Assert.IsNotEmpty(received, "The ViewModel was not told the index was refused");
            Assert.AreEqual(dropdown.value, received[^1], "The ViewModel was told a value that differs from the dropdown");
        }

        [Test]
        public void InOneWay_TheBinderDoesNotListenToTheDropdown()
        {
            var (dropdown, binder) = NewDropdown(BindMode.OneWay);
            var received = new List<int>();

            ((IReverseBinder<int>)binder).ValueChanged += received.Add;
            binder.Bind(new TwoWayStructBindableMember<int>(0, _ => { }));

            dropdown.value = 2;

            Assert.IsEmpty(received, "In OneWay the binder still listens to the dropdown");
        }

        private (TMP_Dropdown Dropdown, DropdownValueMonoBinder Binder) NewDropdown(BindMode mode)
        {
            var gameObject = Spawn("Dropdown");
            var dropdown = gameObject.AddComponent<TMP_Dropdown>();
            dropdown.options = new List<TMP_Dropdown.OptionData>
            {
                new("One"),
                new("Two"),
                new("Three"),
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
