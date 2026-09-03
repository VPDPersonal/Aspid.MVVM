using TMPro;
using UnityEditor;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the numeric reverse channels of <see cref="InputFieldMonoBinder"/>.
    /// </summary>
    /// <remarks>
    /// Text that parses as a number reaches every numeric channel, each in its own type: the integer ones
    /// drop the fraction, so <c>"5.7"</c> is <c>5</c> there. A whole number is read as a
    /// <see langword="long"/> first, because a <see langword="double"/> holds no <see langword="long"/>
    /// past 2^53 exactly and the long channel exists for numbers that need those bits.
    /// </remarks>
    [TestFixture]
    public sealed class InputFieldNumberChannelTests : SceneFixture
    {
        [Test]
        public void AFractionalValue_ReachesEveryChannel()
        {
            var (binder, field) = Create();

            var integers = new List<int>();
            var decimals = new List<float>();
            ((IReverseBinder<int>)binder).ValueChanged += value => integers.Add(value);
            ((IReverseBinder<float>)binder).ValueChanged += value => decimals.Add(value);

            field.onValueChanged.Invoke("5.7");

            Assert.AreEqual(new[] { 5.7f }, decimals, "The fractional value did not reach the floating-point channel");
            Assert.AreEqual(new[] { 5 }, integers, "The fractional value did not reach the integer channel truncated");
        }

        [Test]
        public void AWholeValueBeyondDoublePrecision_KeepsEveryBitOnTheLongChannel()
        {
            var (binder, field) = Create();

            var received = new List<long>();
            ((IReverseBinder<long>)binder).ValueChanged += value => received.Add(value);

            field.onValueChanged.Invoke("1234567890123456789");

            Assert.AreEqual(new[] { 1234567890123456789L }, received, "The long integer lost precision while parsing");
        }

        [Test]
        public void AWholeValue_ReachesEveryChannel()
        {
            var (binder, field) = Create();

            var integers = new List<int>();
            var decimals = new List<float>();
            ((IReverseBinder<int>)binder).ValueChanged += value => integers.Add(value);
            ((IReverseBinder<float>)binder).ValueChanged += value => decimals.Add(value);

            field.onValueChanged.Invoke("5");

            Assert.AreEqual(new[] { 5 }, integers, "The whole value did not reach the integer channel");
            Assert.AreEqual(new[] { 5f }, decimals, "The whole value did not reach the floating-point channel");
        }

        [Test]
        public void TextThatIsNotANumber_ReachesNoNumericChannel()
        {
            var (binder, field) = Create();

            var integers = new List<int>();
            var decimals = new List<float>();
            ((IReverseBinder<int>)binder).ValueChanged += value => integers.Add(value);
            ((IReverseBinder<float>)binder).ValueChanged += value => decimals.Add(value);

            field.onValueChanged.Invoke("nonsense");

            Assert.IsEmpty(integers, "Non-numeric text reached the integer channel");
            Assert.IsEmpty(decimals, "Non-numeric text reached the floating-point channel");
        }

        private (InputFieldMonoBinder binder, TMP_InputField field) Create()
        {
            var gameObject = Spawn("InputField");
            var field = gameObject.AddComponent<TMP_InputField>();
            field.contentType = TMP_InputField.ContentType.DecimalNumber;

            var binder = gameObject.AddComponent<InputFieldMonoBinder>();

            var serializedObject = new SerializedObject(binder);
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.TwoWay;
            serializedObject.FindProperty("_cultureInfoMode").enumValueIndex = (int)CultureInfoMode.InvariantCulture;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            ((IBinder)binder).Bind(new TwoWayBindableMember<string>(string.Empty, _ => { }));

            return (binder, field);
        }
    }
}
