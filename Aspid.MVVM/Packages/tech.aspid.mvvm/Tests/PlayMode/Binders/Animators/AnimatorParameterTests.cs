#if UNITY_EDITOR
using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using UnityEditor.Animations;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests asserting that an Animator binder refuses a parameter name its controller does not have.
    /// </summary>
    /// <remarks>
    /// These live in PlayMode because an <see cref="Animator"/> only reports its parameters once it is running.
    /// </remarks>
    [TestFixture]
    public sealed class AnimatorParameterTests : SceneFixture
    {
        private const string Existing = "Speed";

        [UnityTest]
        public IEnumerator WithAMisspelledParameterName_TheAnimatorIsLeftAlone()
        {
            LogAssert.Expect(LogType.Error, new Regex("has no Float parameter by that name"));

            var (binder, animator) = Create("Speeed");
            yield return null;

            ((IBinder<float>)binder).SetValue(5f);
            ((IBinder<float>)binder).SetValue(6f);

            Assert.AreEqual(0f, animator.GetFloat(Existing), "The binder touched the animator despite the wrong parameter name");
        }

        [UnityTest]
        public IEnumerator WithTheWrongParameterType_TheAnimatorIsLeftAlone()
        {
            LogAssert.Expect(LogType.Error, new Regex("has no Float parameter by that name"));

            var (binder, animator) = Create("IsJumping", extra: AnimatorControllerParameterType.Bool);
            yield return null;

            ((IBinder<float>)binder).SetValue(5f);

            Assert.IsFalse(animator.GetBool("IsJumping"), "The binder wrote a float into a bool parameter");
        }

        [UnityTest]
        public IEnumerator WithAnExistingParameterName_TheValueReachesTheAnimator()
        {
            var (binder, animator) = Create(Existing);
            yield return null;

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(5f, animator.GetFloat(Existing), "The value did not reach the animator");
        }

        /// <summary>
        /// Builds an animator with a single <c>Speed</c> float parameter — plus <paramref name="extra"/> when the
        /// test needs a parameter of another type — and a float binder pointed at <paramref name="parameterName"/>.
        /// </summary>
        /// <remarks>
        /// The controller gets a layer because an <see cref="Animator"/> never starts on one without a state
        /// machine, and the GameObject is built inactive so the binder is fully configured before <c>OnEnable</c>
        /// runs — in a scene its fields are already deserialized by then.
        /// </remarks>
        private (AnimatorSetFloatMonoBinder binder, Animator animator) Create(
            string parameterName,
            AnimatorControllerParameterType? extra = null)
        {
            var controller = Track(new AnimatorController());
            controller.AddLayer("Base");
            controller.AddParameter(Existing, AnimatorControllerParameterType.Float);

            if (extra is not null)
                controller.AddParameter(parameterName, extra.Value);

            var gameObject = Spawn("Animator");
            gameObject.SetActive(false);

            var animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = controller;

            var binder = gameObject.AddComponent<AnimatorSetFloatMonoBinder>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("<ParameterName>k__BackingField").stringValue = parameterName;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            gameObject.SetActive(true);

            return (binder, animator);
        }
    }
}
#endif
