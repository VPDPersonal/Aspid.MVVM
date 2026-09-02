using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.Events;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the tween binders.
    /// </summary>
    /// <remarks>
    /// <c>Update</c> does not run for a component in an EditMode test, so what is pinned here is everything that
    /// happens without it: the first value passes through instantly, a zero duration forwards every value, and
    /// unbinding forgets the state so the next binding starts from what it is given.
    /// </remarks>
    [TestFixture]
    public sealed class TweenTests : SceneFixture
    {
        /// <summary>
        /// Easing the first value would flash a bar from empty, so the first one after binding is forwarded as-is.
        /// </summary>
        [Test]
        public void TheFirstValue_PassesThroughInstantly()
        {
            var (binder, received) = NewFloatTween(duration: 1f);

            ((IBinder<float>)binder).SetValue(0.75f);

            Assert.AreEqual(new List<float> { 0.75f }, received, "The first value did not pass through at once");
        }

        [Test]
        public void AZeroDuration_ForwardsEveryValue()
        {
            var (binder, received) = NewFloatTween(duration: 0f);

            ((IBinder<float>)binder).SetValue(1f);
            ((IBinder<float>)binder).SetValue(2f);
            ((IBinder<float>)binder).SetValue(3f);

            Assert.AreEqual(new List<float> { 1f, 2f, 3f }, received, "With a zero duration, values did not pass through");
        }

        /// <summary>
        /// A tween that survived unbinding would ease out of the previous view's state when the binder is reused — which
        /// is exactly what a pooled list item does.
        /// </summary>
        [Test]
        public void AfterUnbinding_TheNextValuePassesThroughAgain()
        {
            var (binder, received) = NewFloatTween(duration: 1f);
            var member = new OneWayStructBindableMember<float>(0f);

            binder.Bind(member);
            ((IBinder<float>)binder).SetValue(0.5f);
            binder.Unbind();

            received.Clear();

            // Re-binding publishes the member's value on its own. It must pass through instantly, like the
            // first value after binding — otherwise it would ease instead, and nothing would show up here
            // since Update does not run in an EditMode test.
            binder.Bind(member);

            Assert.AreEqual(new List<float> { 0f }, received, "The tween state was not reset after unbinding");
        }

        [Test]
        public void TheColorAndVectorTweens_ForwardTheirFirstValue()
        {
            var colorBinder = NewBinder<TweenColorMonoBinder>(0f);
            var vectorBinder = NewBinder<TweenVector3MonoBinder>(0f);

            var colors = new List<Color>();
            var vectors = new List<Vector3>();

            Listen(colorBinder, (UnityAction<Color>)colors.Add);
            Listen(vectorBinder, (UnityAction<Vector3>)vectors.Add);

            ((IBinder<Color>)colorBinder).SetValue(Color.red);
            ((IBinder<Vector3>)vectorBinder).SetValue(Vector3.one);

            Assert.AreEqual(new List<Color> { Color.red }, colors, "The color did not pass through the tween");
            Assert.AreEqual(new List<Vector3> { Vector3.one }, vectors, "The vector did not pass through the tween");
        }

        private (TweenFloatMonoBinder Binder, List<float> Received) NewFloatTween(float duration)
        {
            var binder = NewBinder<TweenFloatMonoBinder>(duration);
            var received = new List<float>();

            Listen(binder, (UnityAction<float>)received.Add);
            return (binder, received);
        }

        private T NewBinder<T>(float duration)
            where T : MonoBinder
        {
            var binder = Spawn<T>("Tween");
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_duration").floatValue = duration;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }

        /// <summary>
        /// Subscribes to the binder's serialized event through the field, the way the Inspector's listener list would —
        /// the event is private and the tween base owns it.
        /// </summary>
        private static void Listen<T>(MonoBinder binder, UnityAction<T> listener)
        {
            for (var type = binder.GetType(); type is not null; type = type.BaseType)
            {
                var field = type.GetField("_value", BindingFlags.Instance | BindingFlags.NonPublic);
                if (field is null) continue;

                if (field.GetValue(binder) is not UnityEvent<T> unityEvent)
                {
                    unityEvent = new UnityEvent<T>();
                    field.SetValue(binder, unityEvent);
                }

                unityEvent.AddListener(listener);
                return;
            }

            Assert.Fail("The tween binder has no _value field");
        }
    }
}
