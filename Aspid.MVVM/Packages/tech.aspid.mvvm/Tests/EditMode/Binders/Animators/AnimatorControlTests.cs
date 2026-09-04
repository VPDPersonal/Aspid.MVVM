using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="Animator.speed"/>, a layer's weight, the runtime controller, and playing a named state.
    /// </summary>
    [TestFixture]
    public sealed class AnimatorControlTests : SceneFixture
    {
        /// <summary>
        /// <see cref="Animator.speed"/> cannot be observed outside play mode: an animator with no running playable
        /// graph reports zero whatever is written, with or without a controller. What is pinned here is that both
        /// paths run cleanly; the guard itself is <see cref="BinderMath.IsFinite"/>, which has its own tests.
        /// </summary>
        [Test]
        public void Speed_AcceptsFiniteValuesAndRefusesNonFiniteWithoutThrowing()
        {
            var animator = New();
            var binder = animator.gameObject.AddComponent<AnimatorSpeedMonoBinder>();

            Assert.DoesNotThrow(() => ((IBinder<float>)binder).SetValue(-1f), "A negative speed threw");
            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            Assert.DoesNotThrow(() => ((IBinder<float>)binder).SetValue(float.NaN), "NaN threw");
            Assert.IsFalse(float.IsNaN(animator.speed), "NaN reached the animator");
        }

        [Test]
        public void Controller_ADestroyedControllerArrivesAsNull()
        {
            var animator = New();
            var binder = animator.gameObject.AddComponent<AnimatorControllerMonoBinder>();
            var controller = Track(new UnityEditor.Animations.AnimatorController());

            ((IBinder<RuntimeAnimatorController>)binder).SetValue(controller);
            Assert.AreSame(controller, animator.runtimeAnimatorController, "The live controller did not arrive");

            Destroy(controller);
            ((IBinder<RuntimeAnimatorController>)binder).SetValue(controller);

            Assert.IsFalse(animator.runtimeAnimatorController, "A destroyed controller stayed alive for Unity");
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

            LogAssert.Expect(LogType.Error, new Regex("has no layer 1"));
            ((IBinder<float>)binder).SetValue(0.5f);
        }

        [Test]
        public void PlayState_ABlankNameDoesNothing()
        {
            var animator = New();
            var binder = animator.gameObject.AddComponent<AnimatorPlayStateMonoBinder>();

            Assert.DoesNotThrow(() => ((IBinder<string>)binder).SetValue(null), "A null state name threw");
            Assert.DoesNotThrow(() => ((IBinder<string>)binder).SetValue("   "), "A blank state name threw");
        }

        [Test]
        public void SetAndResetTrigger_ShareTheirPlumbing()
        {
            var animator = New();

            var set = animator.gameObject.AddComponent<AnimatorSetTriggerMonoBinder>();
            var reset = animator.gameObject.AddComponent<AnimatorResetTriggerMonoBinder>();

            Assert.IsInstanceOf<AnimatorTriggerMonoBinder>(set, "Set no longer shares the common base");
            Assert.IsInstanceOf<AnimatorTriggerMonoBinder>(reset, "Reset no longer shares the common base");
            Assert.AreEqual(BindMode.OneWayToSource, reset.Mode, "The default mode is not OneWayToSource");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var animator = New();

            Assert.IsTrue(new AnimatorSetTriggerBinder(animator, "Hit").CanBind);
            Assert.IsTrue(new AnimatorResetTriggerBinder(animator, "Hit").CanBind);
            Assert.IsTrue(new AnimatorSpeedBinder(animator).CanBind);
            Assert.IsTrue(new AnimatorControllerBinder(animator).CanBind);
            Assert.IsTrue(new AnimatorLayerWeightBinder(animator).CanBind);
            Assert.IsTrue(new AnimatorPlayStateBinder(animator).CanBind);
        }

        [Test]
        public void TheStateBinders_RefuseTheReverseModes()
        {
            var animator = New();

            Assert.Throws<System.InvalidOperationException>(
                () => _ = new AnimatorPlayStateBinder(animator, mode: BindMode.OneWayToSource),
                "OneWayToSource was accepted by a mode with nothing to read back");
        }

        private Animator New() =>
            Spawn<Animator>("Animator");
    }
}
