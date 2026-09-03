using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using UnityEngine.TestTools;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the casters that read a bound string through a converter: <see cref="StringToIntCasterMonoBinder"/>,
    /// <see cref="StringToFloatCasterMonoBinder"/> and <see cref="StringToEnumCasterMonoBinder{TEnum}"/>.
    /// </summary>
    [TestFixture]
    public sealed class ParsingCasterTests : SceneFixture
    {
        [Test]
        public void TheIntCaster_ForwardsTheConvertedValue()
        {
            var (binder, received) = NewIntCaster();

            ((IBinder<string>)binder).SetValue("17");

            Assert.AreEqual(new List<int> { 17 }, received, "The converted value did not reach the UnityEvent");
        }

        [Test]
        public void TheIntCaster_ForwardsTheConverterFallbackWhenTheTextIsNotANumber()
        {
            var (binder, received) = NewIntCaster(new StringToIntConverter(fallback: -1));

            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter.*a whole number"));
            ((IBinder<string>)binder).SetValue("abc");

            Assert.AreEqual(new List<int> { -1 }, received, "The fallback did not reach the UnityEvent");
        }

        [Test]
        public void TheIntCaster_WithoutAConverter_LogsAndForwardsNothing()
        {
            var (binder, received) = NewIntCaster(converter: null);

            LogAssert.Expect(LogType.Error, new Regex("no converter is assigned"));
            ((IBinder<string>)binder).SetValue("17");

            Assert.IsEmpty(received, "A value was forwarded without a converter");
        }

        [Test]
        public void TheFloatCaster_ForwardsTheConvertedValue()
        {
            var binder = NewBinder<StringToFloatCasterMonoBinder>();
            var received = new List<float>();

            Listen<float>(binder, received.Add);
            ((IBinder<string>)binder).SetValue("2.5");

            Assert.AreEqual(2.5f, received[0], 0.001f, "The converted value did not reach the UnityEvent");
        }

        /// <summary>
        /// A generic MonoBehaviour cannot be added as a component, so the enum caster is abstract — and its serialized
        /// fields have to survive the closing subclass, which is the part worth checking.
        /// </summary>
        [Test]
        public void TheEnumCaster_KeepsItsSerializedFieldsThroughAConcreteSubclass()
        {
            var binder = NewBinder<StringToBindModeCasterMonoBinder>();
            var serializedObject = new SerializedObject(binder);

            Assert.IsNotNull(serializedObject.FindProperty("_casted"), "The UnityEvent is not serialized in the closed subclass");
            Assert.IsNotNull(serializedObject.FindProperty("_converter"), "The converter is not serialized in the closed subclass");
        }

        [Test]
        public void TheEnumCaster_ForwardsTheConvertedMember()
        {
            var binder = NewBinder<StringToBindModeCasterMonoBinder>();
            var received = new List<BindMode>();

            Listen<BindMode>(binder, received.Add);
            ((IBinder<string>)binder).SetValue("TwoWay");

            Assert.AreEqual(new List<BindMode> { BindMode.TwoWay }, received, "The converted member did not reach the UnityEvent");
        }

        #region Helpers
        private (StringToIntCasterMonoBinder Binder, List<int> Received) NewIntCaster(IConverter<string, int> converter)
        {
            var (binder, received) = NewIntCaster();
            Field(binder, "_converter").SetValue(binder, converter);
            return (binder, received);
        }

        private (StringToIntCasterMonoBinder Binder, List<int> Received) NewIntCaster()
        {
            var binder = NewBinder<StringToIntCasterMonoBinder>();
            var received = new List<int>();

            Listen<int>(binder, received.Add);
            return (binder, received);
        }

        /// <summary>
        /// Subscribes to the binder's serialized <c>UnityEvent</c> the way the Inspector's own listener list does —
        /// through the field, since the event is private and has no public accessor.
        /// </summary>
        private static void Listen<T>(MonoBinder binder, UnityEngine.Events.UnityAction<T> listener)
        {
            var field = Field(binder, "_casted");

            if (field.GetValue(binder) is not UnityEngine.Events.UnityEvent<T> unityEvent)
            {
                unityEvent = new UnityEngine.Events.UnityEvent<T>();
                field.SetValue(binder, unityEvent);
            }

            unityEvent.AddListener(listener);
        }

        /// <summary>
        /// Finds a private field on the binder or any of its bases.
        /// </summary>
        private static FieldInfo Field(MonoBinder binder, string name)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            FieldInfo field = null;
            for (var type = binder.GetType(); type is not null && field is null; type = type.BaseType)
                field = type.GetField(name, flags);

            Assert.IsNotNull(field, $"The binder has no {name} field");
            return field!;
        }

        private T NewBinder<T>()
            where T : MonoBinder =>
            Spawn<T>(typeof(T).Name);
        #endregion
    }

    /// <summary>
    /// Closes <see cref="StringToEnumCasterMonoBinder{TEnum}"/> over <see cref="BindMode"/> — the one-line subclass a
    /// project writes for its own enum, and what makes the abstract binder addable as a component.
    /// </summary>
    internal sealed class StringToBindModeCasterMonoBinder : StringToEnumCasterMonoBinder<BindMode> { }
}
