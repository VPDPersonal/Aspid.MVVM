using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new enum-driven <see cref="Toggle.isOn"/> binders.
    /// </summary>
    /// <remarks>
    /// The Toggle domain had only the plain <c>IsOn</c> binder, so the common shape — an enum in the ViewModel
    /// selecting one toggle out of a set of tabs or modes — had to be expressed as several boolean fields kept in
    /// step by hand. The <c>EnumGroup</c> variant does it in one binder.
    /// <para/>
    /// Both write through <c>SetIsOnWithoutNotify</c>: a programmatic write raises <c>Toggle.onValueChanged</c>
    /// like a click does, and any other binder on the same toggle would read it as user input.
    /// </remarks>
    [TestFixture]
    public sealed class ToggleEnumBinderTests
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
        public void EnumGroupBinder_TurnsOnTheMatchingToggleAndClearsTheRest()
        {
            var first = NewToggle(isOn: true);
            var second = NewToggle(isOn: true);

            var binder = NewGroupBinder(("First", first), ("Second", second));

            ((IBinder<System.Enum>)binder).SetValue(TestGroupEnum.Second);

            Assert.IsFalse(first.isOn, "Невыбранный тоггл остался включённым");
            Assert.IsTrue(second.isOn, "Выбранный тоггл не включился");
        }

        [Test]
        public void EnumGroupBinder_DoesNotReportItsOwnWriteAsAClick()
        {
            var toggle = NewToggle(isOn: false);
            var binder = NewGroupBinder(("First", toggle));

            var clicks = 0;
            toggle.onValueChanged.AddListener(_ => clicks++);

            ((IBinder<System.Enum>)binder).SetValue(TestGroupEnum.First);

            Assert.AreEqual(0, clicks, "Программная запись прозвучала как пользовательский клик");
        }

        private ToggleIsOnEnumGroupMonoBinder NewGroupBinder(params (string key, Toggle element)[] entries)
        {
            var gameObject = NewGameObject();
            var binder = gameObject.AddComponent<ToggleIsOnEnumGroupMonoBinder>();

            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_enumValues._enumType").stringValue =
                typeof(TestGroupEnum).AssemblyQualifiedName;

            serializedObject.FindProperty("_defaultValue").boolValue = false;
            serializedObject.FindProperty("_selectedValue").boolValue = true;

            var values = serializedObject.FindProperty("_enumValues._values");
            values.arraySize = entries.Length;

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = values.GetArrayElementAtIndex(i);

                entry.FindPropertyRelative("_key").stringValue = entries[i].key;
                entry.FindPropertyRelative("_value").objectReferenceValue = entries[i].element;
            }

            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            return binder;
        }

        private Toggle NewToggle(bool isOn)
        {
            var toggle = NewGameObject().AddComponent<Toggle>();
            toggle.isOn = isOn;

            return toggle;
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Toggle");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
