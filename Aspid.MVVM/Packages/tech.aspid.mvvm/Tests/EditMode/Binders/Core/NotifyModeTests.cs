using TMPro;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that <see cref="NotifyMode"/> decides which writes from the ViewModel raise the component's change event.
    /// </summary>
    /// <remarks>
    /// The first value arrives while the binder is binding; <see cref="NotifyMode.AfterBind"/> keeps only that
    /// write silent, so a pooled list item that is bound again does not look like a click.
    /// </remarks>
    [TestFixture]
    public sealed class NotifyModeTests : SceneFixture
    {
        [TestCase(NotifyMode.Always, 1, 2)]
        [TestCase(NotifyMode.AfterBind, 0, 1)]
        [TestCase(NotifyMode.Never, 0, 0)]
        public void ToggleBinder_RaisesOnValueChangedPerMode(NotifyMode notifyMode, int onBind, int afterBind)
        {
            var toggle = Spawn<Toggle>("Toggle");
            toggle.isOn = false;

            var binder = SetNotifyMode(toggle.gameObject.AddComponent<ToggleIsOnMonoBinder>(), notifyMode);

            var events = 0;
            toggle.onValueChanged.AddListener(_ => events++);

            var member = new TwoWayStructBindableMember<bool>(true, _ => { });
            binder.Bind(member);

            Assert.IsTrue(toggle.isOn, "The value from binding did not reach the toggle");
            Assert.AreEqual(onBind, events, "Wrong number of events on bind");

            member.Invoke(false);

            Assert.IsFalse(toggle.isOn, "The later value did not reach the toggle");
            Assert.AreEqual(afterBind, events, "Wrong number of events after bind");
        }

        [Test]
        public void ToggleBinder_InEveryMode_DoesNotEchoItsOwnWrite()
        {
            var toggle = Spawn<Toggle>("Toggle");
            var binder = SetNotifyMode(toggle.gameObject.AddComponent<ToggleIsOnMonoBinder>(), NotifyMode.Always);

            var received = new List<bool>();
            ((IReverseBinder<bool>)binder).ValueChanged += received.Add;

            var member = new TwoWayStructBindableMember<bool>(false, _ => { });
            binder.Bind(member);
            member.Invoke(true);

            Assert.IsEmpty(received, "The binder's own write came back as user input");
        }

        [Test]
        public void ToggleBinder_AfterABindAgain_IsSilentAgain()
        {
            var toggle = Spawn<Toggle>("Toggle");
            var binder = SetNotifyMode(toggle.gameObject.AddComponent<ToggleIsOnMonoBinder>(), NotifyMode.AfterBind);

            binder.Bind(new TwoWayStructBindableMember<bool>(true, _ => { }));
            binder.Unbind();

            var events = 0;
            toggle.onValueChanged.AddListener(_ => events++);

            binder.Bind(new TwoWayStructBindableMember<bool>(false, _ => { }));

            Assert.IsFalse(toggle.isOn, "The value from the second binding did not reach the toggle");
            Assert.AreEqual(0, events, "A reused binder raised onValueChanged on bind");
        }

        [Test]
        public void SliderBinder_AfterBind_IsSilentOnBindOnly()
        {
            var slider = Spawn<Slider>("Slider");
            slider.minValue = 0f;
            slider.maxValue = 10f;

            var binder = SetNotifyMode(slider.gameObject.AddComponent<SliderValueMonoBinder>(), NotifyMode.AfterBind);

            var events = 0;
            slider.onValueChanged.AddListener(_ => events++);

            var member = new TwoWayStructBindableMember<float>(3f, _ => { });
            binder.Bind(member);

            Assert.AreEqual(0, events, "The value from binding raised onValueChanged");

            member.Invoke(5f);

            Assert.AreEqual(5f, slider.value);
            Assert.AreEqual(1, events, "The later value did not raise onValueChanged");
        }

        [Test]
        public void DropdownBinder_Always_RaisesTheEventWithoutEchoingIt()
        {
            var dropdown = Spawn<TMP_Dropdown>("Dropdown");
            dropdown.options = new List<TMP_Dropdown.OptionData> { new("One"), new("Two"), new("Three") };

            var binder = SetNotifyMode(dropdown.gameObject.AddComponent<DropdownValueMonoBinder>(), NotifyMode.Always);
            SetMode(binder, BindMode.TwoWay);

            var events = 0;
            dropdown.onValueChanged.AddListener(_ => events++);

            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            binder.Bind(member);

            var received = new List<int>();
            ((IReverseBinder<int>)binder).ValueChanged += received.Add;

            member.Invoke(2);

            Assert.AreEqual(2, events, "A write from the ViewModel did not raise onValueChanged");
            Assert.IsEmpty(received, "The binder's own write came back as a user choice");
        }

        [Test]
        public void ToggleEnumGroupBinder_AfterBind_IsSilentOnBindOnly()
        {
            var toggle = Spawn<Toggle>("Toggle");
            toggle.isOn = false;

            var binder = Spawn<ToggleIsOnEnumGroupMonoBinder>("Group");
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_enumValues._enumType").stringValue = typeof(TestGroupEnum).AssemblyQualifiedName;
            serializedObject.FindProperty("_selectedValue").boolValue = true;
            serializedObject.FindProperty("_notifyMode").enumValueIndex = (int)NotifyMode.AfterBind;

            var values = serializedObject.FindProperty("_enumValues._values");
            values.arraySize = 1;
            values.GetArrayElementAtIndex(0).FindPropertyRelative("_key").stringValue = nameof(TestGroupEnum.First);
            values.GetArrayElementAtIndex(0).FindPropertyRelative("_value").objectReferenceValue = toggle;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var events = 0;
            toggle.onValueChanged.AddListener(_ => events++);

            SetMode(binder, BindMode.OneWay);

            var member = new OneWayEnumBindableMember<TestGroupEnum>(TestGroupEnum.First);
            binder.Bind(member);

            Assert.IsTrue(toggle.isOn, "The value from binding did not reach the toggle");
            Assert.AreEqual(0, events, "The value from binding raised onValueChanged");

            member.Invoke(TestGroupEnum.Second);

            Assert.IsFalse(toggle.isOn, "The later value did not reach the toggle");
            Assert.AreEqual(1, events, "The later value did not raise onValueChanged");
        }

        [TestCase(typeof(ToggleIsOnMonoBinder), typeof(Toggle))]
        [TestCase(typeof(SliderValueMonoBinder), typeof(Slider))]
        [TestCase(typeof(ScrollbarValueMonoBinder), typeof(Scrollbar))]
        [TestCase(typeof(InputFieldMonoBinder), typeof(TMP_InputField))]
        public void AddedThroughTheInspector_ABinderStartsNever(Type binderType, Type componentType)
        {
            var gameObject = Spawn();
            gameObject.AddComponent(componentType);

            var binder = ObjectFactory.AddComponent(gameObject, binderType);

            Assert.AreEqual(NotifyMode.Never, GetNotifyMode(binder));
        }

        [TestCase(typeof(ToggleIsOnMonoBinder), typeof(Toggle))]
        [TestCase(typeof(SliderValueMonoBinder), typeof(Slider))]
        [TestCase(typeof(ScrollbarValueMonoBinder), typeof(Scrollbar))]
        [TestCase(typeof(InputFieldMonoBinder), typeof(TMP_InputField))]
        public void AnExistingBinder_WithoutTheSerializedField_KeepsAlways(Type binderType, Type componentType)
        {
            var gameObject = Spawn();
            gameObject.AddComponent(componentType);

            gameObject.AddComponent(binderType);

            WithLegacyPrefab(gameObject, loaded =>
                Assert.AreEqual(NotifyMode.Always, GetNotifyMode(loaded.GetComponent(binderType))));
        }

        [Test]
        public void AnExistingDropdownBinder_WithoutTheSerializedField_KeepsSilentWrites()
        {
            var dropdown = Spawn<TMP_Dropdown>("Dropdown");
            dropdown.options = new List<TMP_Dropdown.OptionData> { new("One"), new("Two") };
            dropdown.gameObject.AddComponent<SerializedBinderHolder>().Binder = new DropdownValueBinder(dropdown);

            WithLegacyPrefab(dropdown.gameObject, loaded =>
            {
                var target = loaded.GetComponent<TMP_Dropdown>();
                var binder = (DropdownValueBinder)loaded.GetComponent<SerializedBinderHolder>().Binder;
                var events = 0;
                target.onValueChanged.AddListener(_ => events++);

                binder.SetValue(1);

                Assert.AreEqual(1, target.value, "The value did not reach the deserialized dropdown");
                Assert.AreEqual(0, events, "A legacy dropdown binder stopped writing silently");
            });
        }

        private static void WithLegacyPrefab(GameObject source, Action<GameObject> verify)
        {
            var path = $"Assets/Aspid.NotifyModeTests-{Guid.NewGuid():N}.prefab";
            var serializationMode = EditorSettings.serializationMode;
            GameObject loaded = null;

            try
            {
                EditorSettings.serializationMode = SerializationMode.ForceText;
                PrefabUtility.SaveAsPrefabAsset(source, path);

                var yaml = File.ReadAllText(path);
                var legacy = Regex.Replace(yaml, @"(?m)^[ \t]*_notifyMode:.*\r?\n", string.Empty);
                Assert.AreNotEqual(yaml, legacy, "The prefab did not contain a notify mode to remove");
                File.WriteAllText(path, legacy);
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

                loaded = PrefabUtility.LoadPrefabContents(path);
                verify(loaded);
            }
            finally
            {
                if (loaded) PrefabUtility.UnloadPrefabContents(loaded);
                AssetDatabase.DeleteAsset(path);
                EditorSettings.serializationMode = serializationMode;
            }
        }

        private static NotifyMode GetNotifyMode(UnityEngine.Object binder) =>
            (NotifyMode)new SerializedObject(binder).FindProperty("_notifyMode").enumValueIndex;

        private static TBinder SetNotifyMode<TBinder>(TBinder binder, NotifyMode notifyMode)
            where TBinder : MonoBinder
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_notifyMode").enumValueIndex = (int)notifyMode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }

        private static void SetMode(MonoBinder binder, BindMode mode)
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
