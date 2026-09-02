using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
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
    /// own type. A bindable member of such a type starts out <see langword="null"/> and publishes that value the
    /// moment the binder is added, so <see langword="null"/> is the first thing these binders see.
    /// </remarks>
    [TestFixture]
    public sealed class NullValueTests : SceneFixture
    {
        [Test]
        public void UnityEventStringBinder_WithANullValue_ForwardsAnEmptyString()
        {
            var binder = Spawn().AddComponent<UnityEventStringMonoBinder>();

            string received = null;
            var member = new OneWayBindableMember<TestPayload>(null);

            ((IBinder)binder).Bind(member);
            SerializedEvent(binder).AddListener(value => received = value);

            member.Value = null;

            Assert.AreEqual(string.Empty, received, "A null value was not turned into an empty string");
        }

        [Test]
        public void UnityEventStringBinder_WithAValue_StillForwardsItsText()
        {
            var binder = Spawn().AddComponent<UnityEventStringMonoBinder>();

            string received = null;
            var member = new OneWayBindableMember<TestPayload>(null);

            ((IBinder)binder).Bind(member);
            SerializedEvent(binder).AddListener(value => received = value);

            member.Value = new TestPayload();

            Assert.AreEqual("payload", received, "An ordinary value stopped reaching the listener");
        }

        [Test]
        public void DebugLogBinder_WithANullValue_LogsInsteadOfThrowing()
        {
            var binder = Spawn().AddComponent<DebugLogMonoBinder>();
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

            Assert.IsNotNull(field, "The converter field was renamed — this test no longer checks anything");
            Assert.IsNotNull(field.GetValue(binder), "The constructor wiped out the default converter");
        }

        /// <summary>
        /// The binder's <see cref="UnityEvent{T}"/> is a private serialized field — Unity creates the instance,
        /// and a test can only reach it by reflection.
        /// </summary>
        private static UnityEvent<string> SerializedEvent(UnityEventStringMonoBinder binder)
        {
            var field = typeof(UnityEventStringMonoBinder)
                .GetField("_set", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(field, "The event field was renamed — this test no longer checks anything");

            // AddComponent in EditMode does not create the serialized UnityEvent instance — supply our own.
            if (field.GetValue(binder) is not UnityEvent<string> unityEvent)
            {
                unityEvent = new UnityEvent<string>();
                field.SetValue(binder, unityEvent);
            }

            return unityEvent;
        }
    }
}
