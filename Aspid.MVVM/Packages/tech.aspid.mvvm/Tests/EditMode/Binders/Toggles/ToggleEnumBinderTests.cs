using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the enum-driven <see cref="Toggle.isOn"/> group binder.
    /// </summary>
    /// <remarks>
    /// Writes through <c>SetIsOnWithoutNotify</c>: a programmatic write raises <c>Toggle.onValueChanged</c> like a
    /// click does, so any other binder on the same toggle would otherwise read it as user input.
    /// </remarks>
    [TestFixture]
    public sealed class ToggleEnumBinderTests : SceneFixture
    {
        [Test]
        public void EnumGroupBinder_TurnsOnTheMatchingToggleAndClearsTheRest()
        {
            var first = NewToggle(isOn: true);
            var second = NewToggle(isOn: true);

            var binder = NewGroupBinder(("First", first), ("Second", second));

            ((IBinder<System.Enum>)binder).SetValue(TestGroupEnum.Second);

            Assert.IsFalse(first.isOn, "The unselected toggle stayed on");
            Assert.IsTrue(second.isOn, "The selected toggle did not turn on");
        }

        [Test]
        public void EnumGroupBinder_DoesNotReportItsOwnWriteAsAClick()
        {
            var toggle = NewToggle(isOn: false);
            var binder = NewGroupBinder(("First", toggle));

            var clicks = 0;
            toggle.onValueChanged.AddListener(_ => clicks++);

            ((IBinder<System.Enum>)binder).SetValue(TestGroupEnum.First);

            Assert.AreEqual(0, clicks, "The programmatic write was reported as a user click");
        }

        private ToggleIsOnEnumGroupMonoBinder NewGroupBinder(params (string key, Toggle element)[] entries)
        {
            var binder = Spawn<ToggleIsOnEnumGroupMonoBinder>("Toggle");

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
            var toggle = Spawn<Toggle>("Toggle");
            toggle.isOn = isOn;

            return toggle;
        }
    }
}
