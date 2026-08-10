#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.Animations;
using Aspid.MVVM.StarterKit;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for two Animator binder defects that only show up while the animator is running.
    /// </summary>
    /// <remarks>
    /// The integer binder compared the incoming value with the current one through <c>Mathf.Approximately</c>,
    /// whose tolerance is relative: past roughly a million it exceeds 1, so a change of one counts as no change and
    /// the write is skipped. Past 2^24 the two operands are indistinguishable as <c>float</c> at all.
    /// <para/>
    /// <c>OnEnable</c> re-applied the stored value unconditionally, and that value starts at <c>default(T)</c> — so
    /// disabling and re-enabling the object wrote a zero into the Animator over whatever the ViewModel had set,
    /// and told no one.
    /// </remarks>
    [TestFixture]
    public sealed class AnimatorParameterSemanticsTests
    {
        private const string Score = "Score";

        private readonly List<Object> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var spawned in _spawned)
            {
                if (spawned) Object.Destroy(spawned);
            }

            _spawned.Clear();
        }

        [UnityTest]
        public IEnumerator IntBinder_AtLargeValues_StillAppliesAChangeOfOne()
        {
            var (binder, animator) = Create();
            yield return null;

            ((IBinder<int>)binder).SetValue(1_000_000);
            Assert.AreEqual(1_000_000, animator.GetInteger(Score), "Исходное значение не установилось");

            ((IBinder<int>)binder).SetValue(1_000_001);

            Assert.AreEqual(1_000_001, animator.GetInteger(Score), "Изменение на единицу потерялось");
        }

        [UnityTest]
        public IEnumerator IntBinder_BeyondFloatPrecision_StillApplies()
        {
            var (binder, animator) = Create();
            yield return null;

            ((IBinder<int>)binder).SetValue(16_777_216);
            ((IBinder<int>)binder).SetValue(16_777_217);

            Assert.AreEqual(16_777_217, animator.GetInteger(Score), "Значение за пределами точности float потерялось");
        }

        [UnityTest]
        public IEnumerator ReEnabling_BeforeAnyValueArrives_LeavesTheParameterAlone()
        {
            var (binder, animator) = Create();
            yield return null;

            animator.SetInteger(Score, 42);

            // Выключается сам биндер, а не GameObject: Unity сбрасывает параметры аниматора при его отключении,
            // и через GameObject этот тест мерил бы поведение Unity, а не биндера.
            binder.enabled = false;
            binder.enabled = true;
            yield return null;

            Assert.AreEqual(42, animator.GetInteger(Score), "Повторное включение затёрло параметр нулём");
        }

        [UnityTest]
        public IEnumerator ReEnabling_AfterAValueArrives_RestoresIt()
        {
            var (binder, animator) = Create();
            yield return null;

            ((IBinder<int>)binder).SetValue(7);
            animator.SetInteger(Score, 99);

            binder.enabled = false;
            binder.enabled = true;
            yield return null;

            Assert.AreEqual(7, animator.GetInteger(Score), "Последнее значение из ViewModel не восстановлено");
        }

        private (AnimatorSetIntMonoBinder binder, Animator animator) Create()
        {
            var controller = new AnimatorController();
            controller.AddLayer("Base");
            controller.AddParameter(Score, AnimatorControllerParameterType.Int);
            _spawned.Add(controller);

            var gameObject = new GameObject("Animator");
            gameObject.SetActive(false);
            _spawned.Add(gameObject);

            var animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = controller;

            var binder = gameObject.AddComponent<AnimatorSetIntMonoBinder>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("<ParameterName>k__BackingField").stringValue = Score;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            gameObject.SetActive(true);

            return (binder, animator);
        }
    }
}
#endif
