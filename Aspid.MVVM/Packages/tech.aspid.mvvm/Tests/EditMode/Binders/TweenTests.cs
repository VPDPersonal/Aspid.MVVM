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
    /// Tests for the tween binders — the first interpolation in the package.
    /// </summary>
    /// <remarks>
    /// There was no <c>Lerp</c> anywhere in the binder set, so a health bar bound to a health value jumped. The usual
    /// workaround holds a second animated value in the ViewModel and drives it from an update loop, which puts frame-rate
    /// concerns into the layer that is supposed to be free of them.
    /// <para/>
    /// <c>Update</c> does not run for a component in an EditMode test, so what is pinned here is everything that happens
    /// without it: the first value passes through instantly, a zero duration forwards every value, and unbinding forgets
    /// the state so the next binding starts from what it is given.
    /// </remarks>
    [TestFixture]
    public sealed class TweenTests
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
        /// Easing the first value would flash a bar from empty, so the first one after binding is forwarded as-is.
        /// </summary>
        [Test]
        public void TheFirstValue_PassesThroughInstantly()
        {
            var (binder, received) = NewFloatTween(duration: 1f);

            ((IBinder<float>)binder).SetValue(0.75f);

            Assert.AreEqual(new List<float> { 0.75f }, received, "Первое значение не прошло сразу");
        }

        [Test]
        public void AZeroDuration_ForwardsEveryValue()
        {
            var (binder, received) = NewFloatTween(duration: 0f);

            ((IBinder<float>)binder).SetValue(1f);
            ((IBinder<float>)binder).SetValue(2f);
            ((IBinder<float>)binder).SetValue(3f);

            Assert.AreEqual(new List<float> { 1f, 2f, 3f }, received, "С нулевой длительностью значения не проходят");
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

            // Повторная привязка сама публикует значение члена. Оно должно пройти мгновенно, как первое
            // после связывания, — именно это и доказывает, что состояние твина сброшено: иначе значение
            // ушло бы в интерполяцию и в списке не появилось бы вовсе (Update в EditMode-тесте не идёт).
            binder.Bind(member);

            Assert.AreEqual(new List<float> { 0f }, received, "После отвязки состояние твина не сброшено");
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

            Assert.AreEqual(new List<Color> { Color.red }, colors, "Цвет не прошёл через твин");
            Assert.AreEqual(new List<Vector3> { Vector3.one }, vectors, "Вектор не прошёл через твин");
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
            var gameObject = new GameObject("Tween");
            _spawned.Add(gameObject);

            var binder = gameObject.AddComponent<T>();
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

            Assert.Fail("У твин-биндера нет поля _value");
        }
    }
}
