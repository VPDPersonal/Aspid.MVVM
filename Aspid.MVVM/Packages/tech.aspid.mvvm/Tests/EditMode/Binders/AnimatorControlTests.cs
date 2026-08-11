using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the animator properties the domain had left out: <see cref="Animator.speed"/>, a layer's weight, the
    /// runtime controller, and playing a named state.
    /// </summary>
    /// <remarks>
    /// The domain bound the parameters a controller reads and neither the clock it reads them on nor the controller
    /// itself, so slow motion, an additive layer fade and a rig that changes its animation set all lived outside the
    /// framework.
    /// </remarks>
    [TestFixture]
    public sealed class AnimatorControlTests
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

        /// <summary>
        /// The speed binder keeps negative values — playing backwards is legal — and refuses a non-finite one, which
        /// stops the animator dead with nothing logged.
        /// </summary>
        /// <remarks>
        /// <see cref="Animator.speed"/> cannot be observed outside play mode: an animator with no running playable
        /// graph reports zero whatever is written, with or without a controller. What is pinned here is that both
        /// paths run cleanly; the guard itself is <see cref="BinderMath.IsFinite"/>, which has its own tests.
        /// </remarks>
        [Test]
        public void Speed_AcceptsFiniteValuesAndRefusesNonFiniteWithoutThrowing()
        {
            var animator = New();
            var binder = animator.gameObject.AddComponent<AnimatorSpeedMonoBinder>();

            Assert.DoesNotThrow(() => ((IBinder<float>)binder).SetValue(-1f), "Отрицательная скорость уронила биндер");
            Assert.DoesNotThrow(() => ((IBinder<float>)binder).SetValue(float.NaN), "NaN уронил биндер");
            Assert.IsFalse(float.IsNaN(animator.speed), "NaN дошёл до аниматора");
        }

        [Test]
        public void Controller_ADestroyedControllerArrivesAsNull()
        {
            var animator = New();
            var binder = animator.gameObject.AddComponent<AnimatorControllerMonoBinder>();
            var controller = new UnityEditor.Animations.AnimatorController();

            try
            {
                ((IBinder<RuntimeAnimatorController>)binder).SetValue(controller);
                Assert.AreSame(controller, animator.runtimeAnimatorController, "Живой контроллер не доехал");

                Object.DestroyImmediate(controller);
                ((IBinder<RuntimeAnimatorController>)binder).SetValue(controller);

                Assert.IsFalse(animator.runtimeAnimatorController, "Уничтоженный контроллер остался живым для Unity");
            }
            finally
            {
                if (controller) Object.DestroyImmediate(controller);
            }
        }

        /// <summary>
        /// A layer index the controller does not have is reported rather than silently ignored — an animator with no
        /// controller has no layers at all, which is exactly the case a misconfigured binder hits.
        /// </summary>
        [Test]
        public void LayerWeight_ALayerThatDoesNotExist_IsReported()
        {
            var animator = New();
            var binder = animator.gameObject.AddComponent<AnimatorLayerWeightMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("Layer 0 does not exist"));
            ((IBinder<float>)binder).SetValue(0.5f);
        }

        [Test]
        public void PlayState_ABlankNameDoesNothing()
        {
            var animator = New();
            var binder = animator.gameObject.AddComponent<AnimatorPlayStateMonoBinder>();

            Assert.DoesNotThrow(() => ((IBinder<string>)binder).SetValue(null), "Null-имя состояния уронило биндер");
            Assert.DoesNotThrow(() => ((IBinder<string>)binder).SetValue("   "), "Пустое имя состояния уронило биндер");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var animator = New();

            Assert.IsTrue(new AnimatorSpeedBinder(animator).IsBind);
            Assert.IsTrue(new AnimatorControllerBinder(animator).IsBind);
            Assert.IsTrue(new AnimatorLayerWeightBinder(animator).IsBind);
            Assert.IsTrue(new AnimatorPlayStateBinder(animator).IsBind);
        }

        [Test]
        public void TheStateBinders_RefuseTheReverseModes()
        {
            var animator = New();

            Assert.Throws<System.InvalidOperationException>(
                () => _ = new AnimatorPlayStateBinder(animator, mode: BindMode.OneWayToSource),
                "OneWayToSource принят режимом, в котором нечего читать обратно");
        }

        private Animator New()
        {
            var gameObject = new GameObject("Animator");
            _spawned.Add(gameObject);

            return gameObject.AddComponent<Animator>();
        }
    }
}
