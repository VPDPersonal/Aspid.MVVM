using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the aggregators — the answer to a question that depends on several members at once.
    /// </summary>
    /// <remarks>
    /// A binder binds one member, which is the right shape for a value and the wrong one for "is the button available",
    /// whose answer depends on three of them. The usual workaround adds a fourth field to the ViewModel that exists to
    /// hold the answer, moving view logic one layer down.
    /// <para/>
    /// The shape chosen here keeps the framework's rule intact: each input is an ordinary binder bound to its own member,
    /// and they write into a shared aggregator under their own indices.
    /// </remarks>
    [TestFixture]
    public sealed class AggregatorTests
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
        /// A partial answer is worse than a late one: with an And over three conditions, two arriving first would enable a
        /// button the third immediately disables.
        /// </summary>
        [Test]
        public void NothingIsForwarded_UntilEveryInputHasReported()
        {
            var (aggregator, received) = NewAnd(inputs: 3);

            aggregator.SetInput(0, true);
            aggregator.SetInput(1, true);

            Assert.IsEmpty(received, "Результат ушёл до того, как отчитались все входы");

            aggregator.SetInput(2, true);

            Assert.AreEqual(new List<bool> { true }, received, "Результат не ушёл после последнего входа");
        }

        [Test]
        public void And_IsFalseWhenAnyInputIsFalse()
        {
            var (aggregator, received) = NewAnd(inputs: 2);

            aggregator.SetInput(0, true);
            aggregator.SetInput(1, false);

            Assert.AreEqual(new List<bool> { false }, received, "And вернул true при ложном входе");
        }

        [Test]
        public void Or_IsTrueWhenAnyInputIsTrue()
        {
            var (aggregator, received) = New<OrBoolMonoBinder, bool>(inputs: 2);

            aggregator.SetInput(0, false);
            aggregator.SetInput(1, true);

            Assert.AreEqual(new List<bool> { true }, received, "Or вернул false при истинном входе");
        }

        [Test]
        public void Format_ComposesTheParts_AndTreatsNullAsEmpty()
        {
            var (aggregator, received) = New<FormatStringMonoBinder, string>(inputs: 2);
            var serializedObject = new UnityEditor.SerializedObject(aggregator);

            serializedObject.FindProperty("_format").stringValue = "{0} / {1}";
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            aggregator.SetInput(0, "3");
            aggregator.SetInput(1, null);

            Assert.AreEqual(new List<string> { "3 / " }, received, "Null-часть попала в строку не как пустая");
        }

        /// <summary>
        /// A format that does not match the inputs is a configuration mistake; throwing inside a binding loop would take
        /// the rest of the View's bindings with it.
        /// </summary>
        [Test]
        public void AFormatThatDoesNotMatch_IsReportedRatherThanThrown()
        {
            var (aggregator, _) = New<FormatStringMonoBinder, string>(inputs: 1);
            var serializedObject = new UnityEditor.SerializedObject(aggregator);

            serializedObject.FindProperty("_format").stringValue = "{0} {1}";
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            LogAssert.Expect(LogType.Error, new Regex("does not match 1 inputs"));
            Assert.DoesNotThrow(() => aggregator.SetInput(0, "one"));
        }

        /// <summary>
        /// An index the aggregator was not configured for means an input binder and the aggregator disagree about the
        /// shape — and the missing value would keep the result from ever being forwarded.
        /// </summary>
        [Test]
        public void AnIndexOutsideTheConfiguredCount_IsReported()
        {
            var (aggregator, _) = NewAnd(inputs: 2);

            LogAssert.Expect(LogType.Error, new Regex("outside the configured count"));
            aggregator.SetInput(5, true);
        }

        [Test]
        public void AnInputWithoutAnAggregator_SaysSo()
        {
            var gameObject = NewGameObject();
            var input = gameObject.AddComponent<BoolAggregatorInputMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("No aggregator assigned"));
            ((IBinder<bool>)input).SetValue(true);
        }

        [Test]
        public void TheConditionalBinder_ChoosesBetweenTwoConfiguredValues()
        {
            var gameObject = NewGameObject();
            var binder = gameObject.AddComponent<ConditionalStringMonoBinder>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_whenTrue").stringValue = "On";
            serializedObject.FindProperty("_whenFalse").stringValue = "Off";
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var received = new List<string>();
            Listen<string>(binder, "_value", received.Add);

            ((IBinder<bool>)binder).SetValue(true);
            ((IBinder<bool>)binder).SetValue(false);

            Assert.AreEqual(new List<string> { "On", "Off" }, received, "Условный биндер выбрал не те значения");
        }

        private (AndBoolMonoBinder Aggregator, List<bool> Received) NewAnd(int inputs) =>
            New<AndBoolMonoBinder, bool>(inputs);

        private (T Aggregator, List<TResult> Received) New<T, TResult>(int inputs)
            where T : MonoBehaviour
        {
            var gameObject = NewGameObject();
            var aggregator = gameObject.AddComponent<T>();
            var serializedObject = new UnityEditor.SerializedObject(aggregator);

            serializedObject.FindProperty("_inputCount").intValue = inputs;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var received = new List<TResult>();
            Listen<TResult>(aggregator, "_result", received.Add);

            return (aggregator, received);
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

            Assert.Fail($"У объекта нет поля {fieldName}");
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Aggregator");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
