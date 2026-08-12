using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Reflection;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A model type with no dedicated binder overload, so binding it takes the <see cref="IAnyBinder"/> path.
    /// </summary>
    internal sealed class TestPayload
    {
        public override string ToString() => "payload";
    }

    /// <summary>
    /// Regression tests for the binders that accept any bound type and dereferenced the value to print it.
    /// </summary>
    /// <remarks>
    /// <see cref="IAnyBinder.SetValue{T}"/> is the path taken whenever a binder has no overload for the member's
    /// own type — every reference type other than the ones spelled out. A bindable member of such a type starts out
    /// <see langword="null"/> and publishes that value the moment the binder is added, so <see langword="null"/> is
    /// the first thing these binders see rather than an edge case they might never meet.
    /// </remarks>
    [TestFixture]
    public sealed class NullValueTests
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
        public void UnityEventStringBinder_WithANullValue_ForwardsAnEmptyString()
        {
            var binder = NewGameObject().AddComponent<UnityEventStringMonoBinder>();

            string received = null;
            var member = new OneWayBindableMember<TestPayload>(null);

            ((IBinder)binder).Bind(member);
            SerializedEvent(binder).AddListener(value => received = value);

            member.Value = null;

            Assert.AreEqual(string.Empty, received, "Null-значение не превратилось в пустую строку");
        }

        [Test]
        public void UnityEventStringBinder_WithAValue_StillForwardsItsText()
        {
            var binder = NewGameObject().AddComponent<UnityEventStringMonoBinder>();

            string received = null;
            var member = new OneWayBindableMember<TestPayload>(null);

            ((IBinder)binder).Bind(member);
            SerializedEvent(binder).AddListener(value => received = value);

            member.Value = new TestPayload();

            Assert.AreEqual("payload", received, "Обычное значение перестало доезжать");
        }

        [Test]
        public void DebugLogBinder_WithANullValue_LogsInsteadOfThrowing()
        {
            var binder = NewGameObject().AddComponent<DebugLogMonoBinder>();
            var member = new OneWayBindableMember<TestPayload>(null);

            LogAssert.Expect(LogType.Log, new Regex("SetValue: null"));
            ((IBinder)binder).Bind(member);
        }

        /// <summary>
        /// The constructor assigned its parameter straight over the field initializer, so the default converter
        /// its own documentation promises was never applied.
        /// </summary>
        /// <remarks>
        /// Checked by reflection rather than through a log message: with or without the converter the text of an
        /// ordinary value comes out the same, so only the field itself distinguishes the two.
        /// </remarks>
        [Test]
        public void DebugLogBinder_BuiltInCode_KeepsItsDefaultConverter()
        {
            var binder = new DebugLogBinder();

            var field = typeof(DebugLogBinder)
                .GetField("_converter", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(field, "Поле конвертера переименовано — тест больше ничего не проверяет");
            Assert.IsNotNull(field.GetValue(binder), "Конструктор затёр конвертер по умолчанию");
        }

        /// <summary>
        /// The binder's <see cref="UnityEvent{T}"/> is a private serialized field — Unity creates the instance,
        /// and a test can only reach it by reflection.
        /// </summary>
        private static UnityEvent<string> SerializedEvent(UnityEventStringMonoBinder binder)
        {
            var field = typeof(UnityEventStringMonoBinder)
                .GetField("_set", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(field, "Поле события переименовано — тест больше ничего не проверяет");

            // AddComponent в EditMode не создаёт экземпляр сериализуемого UnityEvent — подставляем свой.
            if (field.GetValue(binder) is not UnityEvent<string> unityEvent)
            {
                unityEvent = new UnityEvent<string>();
                field.SetValue(binder, unityEvent);
            }

            return unityEvent;
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("NullValue");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
