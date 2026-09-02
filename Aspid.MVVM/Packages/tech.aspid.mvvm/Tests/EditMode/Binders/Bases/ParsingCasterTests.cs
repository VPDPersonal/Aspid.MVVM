using UnityEditor;
using NUnit.Framework;
using System.Globalization;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the casters that parse a bound string: <see cref="StringToIntCasterMonoBinder"/>,
    /// <see cref="StringToFloatCasterMonoBinder"/> and <see cref="StringToEnumCasterMonoBinder{TEnum}"/>.
    /// </summary>
    [TestFixture]
    public sealed class ParsingCasterTests : SceneFixture
    {
        #region Parsing itself
        [Test]
        public void AnInteger_Parses()
        {
            Assert.IsTrue(StringNumberParse.TryInt("42", out var parsed));
            Assert.AreEqual(42, parsed);
        }

        [Test]
        public void AHalfTypedNumber_DoesNotParse()
        {
            Assert.IsFalse(StringNumberParse.TryInt("-", out _), "A lone minus sign parsed as a number");
            Assert.IsFalse(StringNumberParse.TryInt(string.Empty, out _), "An empty string parsed as a number");
            Assert.IsFalse(StringNumberParse.TryInt(null, out _), "null parsed as a number");
            Assert.IsFalse(StringNumberParse.TryFloat("1,2,3", out _), "Garbage parsed as a number");
        }

        [Test]
        public void NullOrWhitespace_FailAndLeaveTheOutParameterAtZero()
        {
            Assert.IsFalse(StringNumberParse.TryInt(null, out var intFromNull), "null parsed as an int");
            Assert.AreEqual(0, intFromNull);

            Assert.IsFalse(StringNumberParse.TryInt("   ", out var intFromWhitespace), "Whitespace parsed as an int");
            Assert.AreEqual(0, intFromWhitespace);

            Assert.IsFalse(StringNumberParse.TryFloat(null, out var floatFromNull), "null parsed as a float");
            Assert.AreEqual(0f, floatFromNull);

            Assert.IsFalse(StringNumberParse.TryFloat("   ", out var floatFromWhitespace), "Whitespace parsed as a float");
            Assert.AreEqual(0f, floatFromWhitespace);
        }

        /// <summary>
        /// The invariant form has to parse whatever the current culture is, or a game shipped in a comma-decimal
        /// locale would refuse the numbers its own code produced.
        /// </summary>
        [Test]
        public void TheInvariantForm_ParsesUnderACommaDecimalCulture()
        {
            var previous = CultureInfo.CurrentCulture;

            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

                Assert.IsTrue(StringNumberParse.TryFloat("1.5", out var invariant), "The invariant form did not parse");
                Assert.AreEqual(1.5f, invariant, 0.001f);

                Assert.IsTrue(StringNumberParse.TryFloat("1,5", out var local), "The local form did not parse");
                Assert.AreEqual(1.5f, local, 0.001f);
            }
            finally
            {
                CultureInfo.CurrentCulture = previous;
            }
        }

        /// <summary>
        /// <c>NaN</c> and <c>Infinity</c> are words float parsing accepts, and a clamp downstream cannot stop them —
        /// every comparison against <c>NaN</c> is false.
        /// </summary>
        [Test]
        public void NaNAndInfinity_AreRefusedEvenThoughTheyParse()
        {
            Assert.IsFalse(StringNumberParse.TryFloat("NaN", out _), "NaN passed parsing");
            Assert.IsFalse(StringNumberParse.TryFloat("Infinity", out _), "Infinity passed parsing");
        }

        [Test]
        public void AnEnumName_ParsesCaseInsensitively()
        {
            Assert.IsTrue(EnumNameParse.TryName("onetime", out BindMode parsed));
            Assert.AreEqual(BindMode.OneTime, parsed);
        }

        /// <summary>
        /// <see cref="System.Enum.TryParse{TEnum}(string, bool, out TEnum)"/> accepts any number, including one no
        /// member has — an enum holding an undefined value fails later and elsewhere.
        /// </summary>
        [Test]
        public void ANumericString_IsRefusedAsAnEnum()
        {
            Assert.IsFalse(EnumNameParse.TryName("99", out BindMode _), "A number parsed as an enum member");
            Assert.IsFalse(EnumNameParse.TryName("1", out BindMode _), "A number parsed as an enum member");
        }

        [Test]
        public void AnUnknownName_IsRefusedAsAnEnum() =>
            Assert.IsFalse(EnumNameParse.TryName("Sideways", out BindMode _));
        #endregion

        #region The Mono binders
        [Test]
        public void TheIntCaster_ForwardsTheParsedValue()
        {
            var (binder, received) = NewIntCaster();

            ((IBinder<string>)binder).SetValue("17");

            Assert.AreEqual(new List<int> { 17 }, received, "The parsed value did not reach the UnityEvent");
        }

        [Test]
        public void TheIntCaster_ForwardsTheFallbackWhenItDoesNotParse()
        {
            var (binder, received) = NewIntCaster(fallback: -1);

            ((IBinder<string>)binder).SetValue("abc");

            Assert.AreEqual(new List<int> { -1 }, received, "The fallback did not reach the UnityEvent");
        }

        [Test]
        public void TheFloatCaster_ForwardsTheFallbackForANonFiniteValue()
        {
            var binder = NewBinder<StringToFloatCasterMonoBinder>();
            var received = new List<float>();

            Listen<float>(binder, received.Add);
            ((IBinder<string>)binder).SetValue("Infinity");

            Assert.AreEqual(new List<float> { 0f }, received, "The non-finite value passed through instead of the fallback");
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
            Assert.IsNotNull(serializedObject.FindProperty("_fallback"), "The fallback is not serialized in the closed subclass");
        }

        [Test]
        public void TheEnumCaster_ForwardsTheParsedMember()
        {
            var binder = NewBinder<StringToBindModeCasterMonoBinder>();
            var received = new List<BindMode>();

            Listen<BindMode>(binder, received.Add);
            ((IBinder<string>)binder).SetValue("TwoWay");

            Assert.AreEqual(new List<BindMode> { BindMode.TwoWay }, received, "The parsed member did not reach the UnityEvent");
        }
        #endregion

        #region The serializable twins
        [Test]
        public void TheSerializableTwins_ParseAndFallBack()
        {
            var ints = new List<int>();
            var floats = new List<float>();
            var modes = new List<BindMode>();

            new StringToIntCasterBinder(ints.Add, fallback: 5).SetValue("8");
            new StringToIntCasterBinder(ints.Add, fallback: 5).SetValue("eight");
            new StringToFloatCasterBinder(floats.Add, fallback: 0.5f).SetValue("2.5");
            new StringToEnumCasterBinder<BindMode>(modes.Add, BindMode.OneTime).SetValue("nothing");

            Assert.AreEqual(new List<int> { 8, 5 }, ints, "The int caster parsed or fell back incorrectly");
            Assert.AreEqual(2.5f, floats[0], 0.001f, "The float caster parsed incorrectly");
            Assert.AreEqual(new List<BindMode> { BindMode.OneTime }, modes, "The enum caster did not fall back");
        }

        [Test]
        public void TheSerializableTwins_RefuseTheReverseModes()
        {
            Assert.Throws<System.InvalidOperationException>(
                () => _ = new StringToIntCasterBinder(_ => { }, mode: BindMode.OneWayToSource));
        }
        #endregion

        #region Helpers
        private (StringToIntCasterMonoBinder Binder, List<int> Received) NewIntCaster(int fallback = 0)
        {
            var binder = NewBinder<StringToIntCasterMonoBinder>();
            var received = new List<int>();

            if (fallback != 0)
            {
                var serializedObject = new SerializedObject(binder);
                serializedObject.FindProperty("_fallback").intValue = fallback;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }

            Listen<int>(binder, received.Add);
            return (binder, received);
        }

        /// <summary>
        /// Subscribes to the binder's serialized <c>UnityEvent</c> the way the Inspector's own listener list does —
        /// through the field, since the event is private and has no public accessor.
        /// </summary>
        private static void Listen<T>(MonoBinder binder, UnityEngine.Events.UnityAction<T> listener)
        {
            var field = binder.GetType().BaseType is { IsGenericType: true }
                ? binder.GetType().BaseType!.GetField("_casted",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                : binder.GetType().GetField("_casted",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            Assert.IsNotNull(field, "The binder has no _casted field");

            if (field!.GetValue(binder) is not UnityEngine.Events.UnityEvent<T> unityEvent)
            {
                unityEvent = new UnityEngine.Events.UnityEvent<T>();
                field.SetValue(binder, unityEvent);
            }

            unityEvent.AddListener(listener);
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
