using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using Aspid.MVVM.StarterKit;
using Aspid.Collections.Observable;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the collection count binder: "Items: 12" and an empty-state panel, both derived from a collection the
    /// View already has.
    /// </summary>
    /// <remarks>
    /// Neither could be bound before, so the ViewModel carried a count field and an emptiness flag next to the collection
    /// and kept all three in step by hand.
    /// </remarks>
    [TestFixture]
    public sealed class CollectionCountTests
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
        public void APlainList_IsCountedOnce()
        {
            var (binder, counts, empties) = New();

            ((IBinder<IReadOnlyList<object>>)binder).SetValue(new List<object> { "a", "b" });

            Assert.AreEqual(new List<int> { 2 }, counts, "Количество не сообщено");
            Assert.AreEqual(new List<bool> { false }, empties, "Пустота сообщена неверно");
        }

        /// <summary>
        /// The point of the binder: an observable collection is followed, so the count answers every insert and removal
        /// without the ViewModel keeping a second field in step.
        /// </summary>
        [Test]
        public void AnObservableList_IsFollowed()
        {
            var (binder, counts, _) = New();
            var list = new ObservableList<object>();

            ((IBinder<IReadOnlyObservableList<object>>)binder).SetValue(list);
            counts.Clear();

            list.Add("first");
            list.Add("second");
            list.RemoveAt(0);

            Assert.AreEqual(new List<int> { 1, 2, 1 }, counts, "Счётчик не следует за изменениями коллекции");
        }

        /// <summary>
        /// A panel that says "nothing here" is the right answer to a list that has not arrived, so a null collection
        /// reports zero rather than nothing at all.
        /// </summary>
        [Test]
        public void ANullCollection_ReportsZeroAndEmpty()
        {
            var (binder, counts, empties) = New();

            ((IBinder<IReadOnlyList<object>>)binder).SetValue(null);

            Assert.AreEqual(new List<int> { 0 }, counts, "Нулевое количество не сообщено");
            Assert.AreEqual(new List<bool> { true }, empties, "Пустота не сообщена");
        }

        /// <summary>
        /// A binder that kept listening after unbinding would keep answering for a collection the View no longer shows —
        /// and would hold it alive.
        /// </summary>
        [Test]
        public void AfterUnbinding_TheCollectionIsNoLongerFollowed()
        {
            var (binder, counts, _) = New();
            var list = new ObservableList<object>();

            // Через Bind, а не SetValue: отписка живёт в OnUnbound, а он вызывается только у связанного
            // биндера — прямой SetValue оставил бы подписку и тест проверял бы не то.
            binder.Bind(new OneWayBindableMember<IReadOnlyObservableList<object>>(list));
            binder.Unbind();
            counts.Clear();

            list.Add("ignored");

            Assert.IsEmpty(counts, "Биндер продолжил слушать коллекцию после отвязки");
        }

        private (ObjectCollectionCountMonoBinder Binder, List<int> Counts, List<bool> Empties) New()
        {
            var gameObject = new GameObject("Count");
            _spawned.Add(gameObject);

            var binder = gameObject.AddComponent<ObjectCollectionCountMonoBinder>();

            // Компонент, добавленный из кода, не проходит через Reset инспектора и остаётся в TwoWay,
            // которого этот биндер не принимает: выставляем режим так же, как это делает инспектор.
            var serializedObject = new UnityEditor.SerializedObject(binder);
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var counts = new List<int>();
            var empties = new List<bool>();

            Listen<int>(binder, "_count", counts.Add);
            Listen<bool>(binder, "_isEmpty", empties.Add);

            return (binder, counts, empties);
        }

        private static void Listen<TValue>(MonoBehaviour owner, string fieldName, UnityAction<TValue> listener)
        {
            for (var type = owner.GetType(); type is not null; type = type.BaseType)
            {
                var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
                if (field is null) continue;

                if (field.GetValue(owner) is not UnityEvent<TValue> unityEvent)
                {
                    unityEvent = new UnityEvent<TValue>();
                    field.SetValue(owner, unityEvent);
                }

                unityEvent.AddListener(listener);
                return;
            }

            Assert.Fail($"У биндера нет поля {fieldName}");
        }
    }
}
