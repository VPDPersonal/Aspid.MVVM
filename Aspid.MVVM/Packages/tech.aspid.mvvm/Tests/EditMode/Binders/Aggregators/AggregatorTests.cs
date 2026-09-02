using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the aggregators — the answer to a question that depends on several members at once.
    /// </summary>
    [TestFixture]
    public sealed class AggregatorTests : SceneFixture
    {
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

            Assert.IsEmpty(received, "The result went out before every input had reported");

            aggregator.SetInput(2, true);

            Assert.AreEqual(new List<bool> { true }, received, "The result did not go out after the last input");
        }

        [Test]
        public void And_IsFalseWhenAnyInputIsFalse()
        {
            var (aggregator, received) = NewAnd(inputs: 2);

            aggregator.SetInput(0, true);
            aggregator.SetInput(1, false);

            Assert.AreEqual(new List<bool> { false }, received, "And returned true with a false input");
        }

        [Test]
        public void Or_IsTrueWhenAnyInputIsTrue()
        {
            var (aggregator, received) = New<OrBoolMonoBinder, bool>(inputs: 2);

            aggregator.SetInput(0, false);
            aggregator.SetInput(1, true);

            Assert.AreEqual(new List<bool> { true }, received, "Or returned false with a true input");
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

            Assert.AreEqual(new List<string> { "3 / " }, received, "The null part did not land in the string as empty");
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
            var input = Spawn<BoolAggregatorInputMonoBinder>("Aggregator");

            LogAssert.Expect(LogType.Error, new Regex("no aggregator is assigned"));
            ((IBinder<bool>)input).SetValue(true);
        }

        [Test]
        public void TheConditionalBinder_ChoosesBetweenTwoConfiguredValues()
        {
            var binder = Spawn<ConditionalStringMonoBinder>("Aggregator");
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_whenTrue").stringValue = "On";
            serializedObject.FindProperty("_whenFalse").stringValue = "Off";
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var received = new List<string>();
            Listen<string>(binder, "_value", received.Add);

            ((IBinder<bool>)binder).SetValue(true);
            ((IBinder<bool>)binder).SetValue(false);

            Assert.AreEqual(new List<string> { "On", "Off" }, received, "The conditional binder chose the wrong values");
        }

        private (AndBoolMonoBinder Aggregator, List<bool> Received) NewAnd(int inputs) =>
            New<AndBoolMonoBinder, bool>(inputs);

        private (T Aggregator, List<TResult> Received) New<T, TResult>(int inputs)
            where T : Component
        {
            var aggregator = Spawn<T>("Aggregator");
            var serializedObject = new UnityEditor.SerializedObject(aggregator);

            serializedObject.FindProperty("_inputCount").intValue = inputs;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var received = new List<TResult>();
            Listen<TResult>(aggregator, "_result", received.Add);

            return (aggregator, received);
        }

        private static void Listen<TValue>(Component owner, string fieldName, UnityAction<TValue> listener)
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

            Assert.Fail($"The object has no {fieldName} field");
        }
    }
}
