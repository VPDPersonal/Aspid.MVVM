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
    /// Tests for the rate-limiting binders: <see cref="DebounceMonoBinder{T}"/>, <see cref="ThrottleMonoBinder{T}"/> and
    /// <see cref="DelayMonoBinder{T}"/>.
    /// </summary>
    /// <remarks>
    /// <c>Update</c> does not run for a component in an EditMode test, so the policies' timing is driven directly through
    /// the protected <c>Tick</c>.
    /// </remarks>
    [TestFixture]
    public sealed class RateLimitTests : SceneFixture
    {
        [Test]
        public void AZeroInterval_ForwardsEveryValueAtOnce()
        {
            var (binder, received) = New<DebounceStringMonoBinder, string>(seconds: 0f);

            ((IBinder<string>)binder).SetValue("a");
            ((IBinder<string>)binder).SetValue("ab");

            Assert.AreEqual(new List<string> { "a", "ab" }, received, "With a zero interval, values did not pass through at once");
        }

        /// <summary>
        /// A fast typist must produce exactly one forwarded value: every new value restarts the wait.
        /// </summary>
        [Test]
        public void Debounce_ForwardsOnlyTheLastValueOnceTheValuesStop()
        {
            var (binder, received) = New<DebounceStringMonoBinder, string>(seconds: 0.3f);

            ((IBinder<string>)binder).SetValue("s");
            Tick(binder, 0.2f);
            ((IBinder<string>)binder).SetValue("sw");
            Tick(binder, 0.2f);
            ((IBinder<string>)binder).SetValue("swo");

            Assert.IsEmpty(received, "A value was forwarded while input was still ongoing");

            Tick(binder, 0.3f);

            Assert.AreEqual(new List<string> { "swo" }, received, "The last value was not forwarded after the pause");
        }

        /// <summary>
        /// The first value goes through at once — waiting out the interval before showing anything makes the view look
        /// broken — and a value that arrives inside the interval is held until it ends.
        /// </summary>
        [Test]
        public void Throttle_LetsTheFirstValueThroughAndHoldsTheRest()
        {
            var (binder, received) = New<ThrottleFloatMonoBinder, float>(seconds: 0.5f);

            ((IBinder<float>)binder).SetValue(1f);
            Assert.AreEqual(new List<float> { 1f }, received, "The first value did not pass through at once");

            ((IBinder<float>)binder).SetValue(2f);
            ((IBinder<float>)binder).SetValue(3f);
            Assert.AreEqual(new List<float> { 1f }, received, "A value inside the interval passed through early");

            Tick(binder, 0.5f);

            Assert.AreEqual(new List<float> { 1f, 3f }, received, "The last value was not forwarded once the interval ended");
        }

        /// <summary>
        /// Unlike the other two policies a delay drops nothing: every value arrives, in order.
        /// </summary>
        [Test]
        public void Delay_ForwardsEveryValueInOrder()
        {
            var (binder, received) = New<DelayFloatMonoBinder, float>(seconds: 0.2f);

            ((IBinder<float>)binder).SetValue(1f);
            ((IBinder<float>)binder).SetValue(2f);

            Assert.IsEmpty(received, "Values were forwarded before the delay elapsed");

            Tick(binder, 0.2f);

            Assert.AreEqual(new List<float> { 1f, 2f }, received, "The delayed values did not all arrive, or not in order");
        }

        /// <summary>
        /// A value that belonged to the previous binding must not arrive after it — a pooled row would answer for the row
        /// before it.
        /// </summary>
        [Test]
        public void Unbinding_DropsWhateverWasWaiting()
        {
            var (binder, received) = New<DebounceStringMonoBinder, string>(seconds: 0.3f);

            binder.Bind(new OneWayBindableMember<string>(null));
            ((IBinder<string>)binder).SetValue("held");
            binder.Unbind();

            received.Clear();
            Tick(binder, 1f);

            Assert.IsEmpty(received, "A pending value was forwarded after unbinding");
        }

        private (T Binder, List<TValue> Received) New<T, TValue>(float seconds)
            where T : MonoBinder
        {
            var binder = Spawn<T>("RateLimit");
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_seconds").floatValue = seconds;
            // A component added from code skips the inspector's Reset, so the mode defaults to TwoWay,
            // which these binders reject. Set it the way the inspector does.
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var received = new List<TValue>();
            Listen<TValue>(binder, received.Add);

            return (binder, received);
        }

        /// <summary>
        /// Drives the policy's clock directly, because Unity does not call <c>Update</c> on a component in an EditMode
        /// test.
        /// </summary>
        private static void Tick(MonoBinder binder, float deltaTime)
        {
            var method = Method(binder.GetType(), "Tick");
            Assert.IsNotNull(method, "The binder has no Tick method");

            method!.Invoke(binder, new object[] { deltaTime });
        }

        private static void Listen<TValue>(MonoBinder binder, UnityAction<TValue> listener)
        {
            for (var type = binder.GetType(); type is not null; type = type.BaseType)
            {
                var field = type.GetField("_value", BindingFlags.Instance | BindingFlags.NonPublic);
                if (field is null) continue;

                if (field.GetValue(binder) is not UnityEvent<TValue> unityEvent)
                {
                    unityEvent = new UnityEvent<TValue>();
                    field.SetValue(binder, unityEvent);
                }

                unityEvent.AddListener(listener);
                return;
            }

            Assert.Fail("The binder has no _value field");
        }

        private static MethodInfo Method(System.Type type, string name)
        {
            for (; type is not null; type = type.BaseType)
            {
                var method = type.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);
                if (method is not null) return method;
            }

            return null;
        }
    }
}
