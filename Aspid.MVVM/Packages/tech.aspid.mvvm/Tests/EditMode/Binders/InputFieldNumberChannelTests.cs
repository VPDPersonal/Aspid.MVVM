#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
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
    public sealed class InputFieldNumberChannelTests
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
        public void AFractionalValue_ReachesEveryChannel()
        {
            var (binder, field) = Create();

            var integers = new List<int>();
            var decimals = new List<float>();
            ((IReverseBinder<int>)binder).ValueChanged += value => integers.Add(value);
            ((IReverseBinder<float>)binder).ValueChanged += value => decimals.Add(value);

            field.onValueChanged.Invoke("5.7");

            Assert.AreEqual(new[] { 5.7f }, decimals, "Дробное значение не дошло до вещественного канала");
            Assert.AreEqual(new[] { 5 }, integers, "Дробное значение не дошло до целочисленного канала усечённым");
        }

        [Test]
        public void AWholeValueBeyondDoublePrecision_KeepsEveryBitOnTheLongChannel()
        {
            var (binder, field) = Create();

            var received = new List<long>();
            ((IReverseBinder<long>)binder).ValueChanged += value => received.Add(value);

            field.onValueChanged.Invoke("1234567890123456789");

            Assert.AreEqual(new[] { 1234567890123456789L }, received, "Длинное целое потеряло точность при разборе");
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

            Assert.AreEqual(new[] { 5 }, integers, "Целое значение не дошло до целочисленного канала");
            Assert.AreEqual(new[] { 5f }, decimals, "Целое значение не дошло до вещественного канала");
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

            Assert.IsEmpty(integers, "Нечисловой текст попал на целочисленный канал");
            Assert.IsEmpty(decimals, "Нечисловой текст попал на вещественный канал");
        }

        private (InputFieldMonoBinder binder, TMP_InputField field) Create()
        {
            var gameObject = NewGameObject();
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

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("InputField");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
#endif
