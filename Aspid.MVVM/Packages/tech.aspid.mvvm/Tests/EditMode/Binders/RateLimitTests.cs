using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the rate-limiting binders: <see cref="DebounceMonoBinder{T}"/>, <see cref="ThrottleMonoBinder{T}"/> and
    /// <see cref="DelayMonoBinder{T}"/>.
    /// </summary>
    /// <remarks>
    /// A search field publishes a value per keystroke and a high-frequency source publishes one per frame. Nothing could
    /// space those out, so the ViewModel either sent every one — a request per character — or grew a timer of its own.
    /// <para/>
    /// <c>Update</c> does not run for a component in an EditMode test, so the policies' timing is driven directly through
    /// the protected <c>Tick</c>. That is the honest way to test them here, and it is what these tests do.
    /// </remarks>
    [TestFixture]
    public sealed class RateLimitTests
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

        [Test]
        public void AZeroInterval_ForwardsEveryValueAtOnce()
        {
            var (binder, received) = New<DebounceStringMonoBinder, string>(seconds: 0f);

            ((IBinder<string>)binder).SetValue("a");
            ((IBinder<string>)binder).SetValue("ab");

            Assert.AreEqual(new List<string> { "a", "ab" }, received, "С нулевым интервалом значения не проходят сразу");
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

            Assert.IsEmpty(received, "Значение ушло, хотя ввод ещё продолжался");

            Tick(binder, 0.3f);

            Assert.AreEqual(new List<string> { "swo" }, received, "После паузы не ушло последнее значение");
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
            Assert.AreEqual(new List<float> { 1f }, received, "Первое значение не прошло сразу");

            ((IBinder<float>)binder).SetValue(2f);
            ((IBinder<float>)binder).SetValue(3f);
            Assert.AreEqual(new List<float> { 1f }, received, "Значение внутри интервала прошло раньше срока");

            Tick(binder, 0.5f);

            Assert.AreEqual(new List<float> { 1f, 3f }, received, "По окончании интервала ушло не последнее значение");
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

            Assert.IsEmpty(received, "Значения ушли до истечения задержки");

            Tick(binder, 0.2f);

            Assert.AreEqual(new List<float> { 1f, 2f }, received, "Задержанные значения пришли не все или не по порядку");
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

            Assert.IsEmpty(received, "Отложенное значение ушло после отвязки");
        }

        private (T Binder, List<TValue> Received) New<T, TValue>(float seconds)
            where T : MonoBinder
        {
            var gameObject = new GameObject("RateLimit");
            _spawned.Add(gameObject);

            var binder = gameObject.AddComponent<T>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_seconds").floatValue = seconds;
            // Компонент, добавленный из кода, не проходит через Reset инспектора, поэтому режим по умолчанию
            // остаётся TwoWay — а эти биндеры его не принимают. Выставляем так же, как это делает инспектор.
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
            Assert.IsNotNull(method, "У биндера нет метода Tick");

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

            Assert.Fail("У биндера нет поля _value");
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
