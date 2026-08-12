#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.Animations;
using Aspid.MVVM.StarterKit;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests asserting that an Animator binder refuses a parameter name its controller does not have.
    /// </summary>
    /// <remarks>
    /// These live in PlayMode because an <see cref="Animator"/> only reports its parameters once it is running.
    /// The controller is built through <c>UnityEditor.Animations</c> — there is no runtime API for constructing
    /// one — which is why the fixture is compiled under <c>UNITY_EDITOR</c>.
    /// <para/>
    /// The error is expected before the binder is even created: the binder reports as soon as it is enabled,
    /// which is earlier than the first bound value and is the point of the check. It reports once, so the
    /// <see cref="LogAssert.Expect(LogType, Regex)"/> below also pins that the later assignments stay quiet.
    /// </remarks>
    [TestFixture]
    public sealed class AnimatorParameterTests
    {
        private const string Existing = "Speed";

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
        public IEnumerator WithAMisspelledParameterName_TheAnimatorIsLeftAlone()
        {
            LogAssert.Expect(LogType.Error, new Regex("has no Float parameter by that name"));

            var (binder, animator) = Create("Speeed");
            yield return null;

            ((IBinder<float>)binder).SetValue(5f);
            ((IBinder<float>)binder).SetValue(6f);

            Assert.AreEqual(0f, animator.GetFloat(Existing), "Биндер тронул аниматор с неверным именем параметра");
        }

        [UnityTest]
        public IEnumerator WithTheWrongParameterType_TheAnimatorIsLeftAlone()
        {
            LogAssert.Expect(LogType.Error, new Regex("has no Float parameter by that name"));

            var (binder, animator) = Create("IsJumping", extra: AnimatorControllerParameterType.Bool);
            yield return null;

            ((IBinder<float>)binder).SetValue(5f);

            Assert.IsFalse(animator.GetBool("IsJumping"), "Биндер записал float в bool-параметр");
        }

        [UnityTest]
        public IEnumerator WithAnExistingParameterName_TheValueReachesTheAnimator()
        {
            var (binder, animator) = Create(Existing);
            yield return null;

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(5f, animator.GetFloat(Existing), "Значение не доехало до аниматора");
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
            var controller = new AnimatorController();
            controller.AddLayer("Base");
            controller.AddParameter(Existing, AnimatorControllerParameterType.Float);

            if (extra is not null)
                controller.AddParameter(parameterName, extra.Value);

            _spawned.Add(controller);

            var gameObject = new GameObject("Animator");
            gameObject.SetActive(false);
            _spawned.Add(gameObject);

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
