using System;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Three members so a table can hold an empty slot with a filled entry on either side of it.
    /// </summary>
    internal enum TestGroupEnum
    {
        First,
        Second,
        Third,
    }

    /// <summary>
    /// Regression tests for references the inspector is free to leave empty.
    /// </summary>
    /// <remarks>
    /// Both defects share a shape: a constructor guards the reference, the serialized path does not, and the
    /// binder dereferences it anyway on the first value it receives.
    /// <para/>
    /// An <c>EnumGroup</c> table with an unassigned slot threw from inside the loop, so the entries after the empty
    /// one never received their value and the group stayed in a mixed state — a half-edited table is an ordinary
    /// thing to run into.
    /// <para/>
    /// An Animator binder with an empty parameter name addressed the animator by that name anyway. Unity reports
    /// the mistake itself, once per call, so a binder driven by a value that changes every frame produced an error
    /// per frame in the editor and complete silence in a build.
    /// <para/>
    /// The parameter-<em>existence</em> half of the Animator check is not testable here: outside play mode
    /// <c>Animator.isInitialized</c> is false and <c>parameterCount</c> is 0 even with a controller assigned, and
    /// neither <c>Rebind</c> nor <c>Update</c> changes that. It is covered in the PlayMode suite instead.
    /// </remarks>
    [TestFixture]
    public sealed class RequiredReferenceTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) UnityEngine.Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void EnumGroup_WithAnEmptySlot_StillUpdatesTheOtherEntries()
        {
            var applied = NewGameObject("Applied");
            var ignored = NewGameObject("Ignored");

            applied.SetActive(false);
            ignored.SetActive(true);

            var binder = CreateVisibleGroup(("First", applied), ("Second", null), ("Third", ignored));

            LogAssert.Expect(LogType.Error, new Regex("has no GameObject assigned"));
            ((IBinder<Enum>)binder).SetValue(TestGroupEnum.First);

            Assert.IsTrue(applied.activeSelf, "Выбранная запись не получила selected-значение");
            Assert.IsFalse(ignored.activeSelf, "Запись после пустого слота не получила default-значение");
        }

        [Test]
        public void EnumGroup_WithAnEmptySlot_ReportsOnlyOnce()
        {
            var applied = NewGameObject("Applied");
            var binder = CreateVisibleGroup(("First", applied), ("Second", null));

            LogAssert.Expect(LogType.Error, new Regex("has no GameObject assigned"));

            ((IBinder<Enum>)binder).SetValue(TestGroupEnum.First);
            ((IBinder<Enum>)binder).SetValue(TestGroupEnum.Second);
            ((IBinder<Enum>)binder).SetValue(TestGroupEnum.First);
        }

        [Test]
        public void AnimatorBinder_WithAnEmptyParameterName_DoesNotAddressTheAnimator()
        {
            var binder = CreateAnimatorBinder(parameterName: "");

            LogAssert.Expect(LogType.Error, new Regex("no parameter name is set"));
            ((IBinder<float>)binder).SetValue(1f);
        }

        [Test]
        public void AnimatorBinder_WithoutAController_DoesNotAddressTheAnimator()
        {
            var binder = CreateAnimatorBinder(parameterName: "Speed");

            LogAssert.Expect(LogType.Error, new Regex("the Animator has no controller"));
            ((IBinder<float>)binder).SetValue(1f);
        }

        [Test]
        public void AnimatorBinder_ReportsOnlyOnce()
        {
            var binder = CreateAnimatorBinder(parameterName: "");

            LogAssert.Expect(LogType.Error, new Regex("no parameter name is set"));

            ((IBinder<float>)binder).SetValue(1f);
            ((IBinder<float>)binder).SetValue(2f);
            ((IBinder<float>)binder).SetValue(3f);
        }

        /// <summary>
        /// A serialized component reference whose target was deleted arrives in managed code as a wrapper that is
        /// not <see langword="null"/> to C# but points at nothing. <c>CachedComponent</c> tested it with
        /// <c>is not null</c>, so it accepted that wrapper as assigned and never reached the
        /// <c>GetComponent</c> fallback its own documentation promises.
        /// </summary>
        [Test]
        public void CachedComponent_WithABrokenReference_FallsBackToGetComponent()
        {
            var ownerObject = NewGameObject("Binder");
            var localSlider = ownerObject.AddComponent<Slider>();

            var targetObject = NewGameObject("Target");
            var targetSlider = targetObject.AddComponent<Slider>();

            var binder = ownerObject.AddComponent<SliderValueMonoBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_component").objectReferenceValue = targetSlider;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            // Ровно то, что делает удаление компонента в инспекторе: ссылка остаётся, объекта за ней нет.
            UnityEngine.Object.DestroyImmediate(targetObject);

            ((IBinder<float>)binder).SetValue(0.5f);

            Assert.AreEqual(0.5f, localSlider.value, "Биндер не откатился на GetComponent при сломанной ссылке");
        }

        private AnimatorSetFloatMonoBinder CreateAnimatorBinder(string parameterName)
        {
            var gameObject = NewGameObject("Animator");
            gameObject.AddComponent<Animator>();

            var binder = gameObject.AddComponent<AnimatorSetFloatMonoBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("<ParameterName>k__BackingField").stringValue = parameterName;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }

        /// <summary>
        /// Builds a <see cref="GameObjectVisibleEnumGroupMonoBinder"/> whose table maps each named
        /// <see cref="TestGroupEnum"/> member to a GameObject — or to nothing, for the empty-slot case.
        /// </summary>
        private GameObjectVisibleEnumGroupMonoBinder CreateVisibleGroup(params (string key, GameObject element)[] entries)
        {
            var binder = NewGameObject("Binder").AddComponent<GameObjectVisibleEnumGroupMonoBinder>();
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

        private GameObject NewGameObject(string name)
        {
            var gameObject = new GameObject(name);
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
