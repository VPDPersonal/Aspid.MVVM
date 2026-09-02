#if UNITY_EDITOR
using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using UnityEditor.Animations;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for two Animator binder defects that only show up while the animator is running.
    /// </summary>
    /// <remarks>
    /// These run in play mode because an <see cref="Animator"/> only advances its parameters once the player loop
    /// is ticking, which a <c>yield return null</c> needs in order to observe.
    /// </remarks>
    [TestFixture]
    public sealed class AnimatorParameterSemanticsTests : SceneFixture
    {
        private const string Score = "Score";

        [UnityTest]
        public IEnumerator IntBinder_AtLargeValues_StillAppliesAChangeOfOne()
        {
            var (binder, animator) = Create();
            yield return null;

            ((IBinder<int>)binder).SetValue(1_000_000);
            Assert.AreEqual(1_000_000, animator.GetInteger(Score), "The initial value did not apply");

            ((IBinder<int>)binder).SetValue(1_000_001);

            Assert.AreEqual(1_000_001, animator.GetInteger(Score), "A change of one was lost");
        }

        [UnityTest]
        public IEnumerator IntBinder_BeyondFloatPrecision_StillApplies()
        {
            var (binder, animator) = Create();
            yield return null;

            ((IBinder<int>)binder).SetValue(16_777_216);
            ((IBinder<int>)binder).SetValue(16_777_217);

            Assert.AreEqual(16_777_217, animator.GetInteger(Score), "A value beyond float precision was lost");
        }

        [UnityTest]
        public IEnumerator ReEnabling_BeforeAnyValueArrives_LeavesTheParameterAlone()
        {
            var (binder, animator) = Create();
            yield return null;

            animator.SetInteger(Score, 42);

            // Disables the binder itself, not the GameObject: Unity resets an animator's parameters when it is
            // disabled, and going through the GameObject would measure that instead of the binder.
            binder.enabled = false;
            binder.enabled = true;
            yield return null;

            Assert.AreEqual(42, animator.GetInteger(Score), "Re-enabling overwrote the parameter with zero");
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

            Assert.AreEqual(7, animator.GetInteger(Score), "The last value from the ViewModel was not restored");
        }

        private (AnimatorSetIntMonoBinder binder, Animator animator) Create()
        {
            var controller = Track(new AnimatorController());
            controller.AddLayer("Base");
            controller.AddParameter(Score, AnimatorControllerParameterType.Int);

            var gameObject = Spawn("Animator");
            gameObject.SetActive(false);

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
