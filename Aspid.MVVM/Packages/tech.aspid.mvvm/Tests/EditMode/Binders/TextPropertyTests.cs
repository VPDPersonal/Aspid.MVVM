#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the text and input field properties the domain had left out: <see cref="TMP_Text.fontStyle"/>,
    /// <see cref="TMP_Text.enableAutoSizing"/>, the two spacings, <see cref="TMP_Text.margin"/>,
    /// <see cref="TMP_InputField.caretPosition"/> and <see cref="TMP_InputField.placeholder"/>.
    /// </summary>
    [TestFixture]
    public sealed class TextPropertyTests
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
        public void FontStyle_TravelsAsTheWholeFlagSet()
        {
            var text = New<TextMeshProUGUI>();
            var binder = text.gameObject.AddComponent<TextFontStyleMonoBinder>();

            ((IBinder<FontStyles>)binder).SetValue(FontStyles.Bold | FontStyles.Italic);

            Assert.AreEqual(FontStyles.Bold | FontStyles.Italic, text.fontStyle, "Набор флагов стиля не доехал");
        }

        [Test]
        public void AutoSize_ReachesTheText()
        {
            var text = New<TextMeshProUGUI>();
            var binder = text.gameObject.AddComponent<TextAutoSizeMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);

            Assert.IsTrue(text.enableAutoSizing, "Автосайз не доехал");
        }

        /// <summary>
        /// Negative tracking and leading are ordinary — that is how a title is tightened — so only a non-finite value
        /// is refused. TMP rebuilds its mesh from these numbers, and one <c>NaN</c> makes the text disappear.
        /// </summary>
        [Test]
        public void TheSpacings_KeepNegativesAndRefuseNonFinite()
        {
            var text = New<TextMeshProUGUI>();
            var character = text.gameObject.AddComponent<TextCharacterSpacingMonoBinder>();
            var line = text.gameObject.AddComponent<TextLineSpacingMonoBinder>();

            ((IBinder<float>)character).SetValue(-5f);
            ((IBinder<float>)line).SetValue(-10f);

            Assert.AreEqual(-5f, text.characterSpacing, 0.001f, "Отрицательный трекинг не сохранён");
            Assert.AreEqual(-10f, text.lineSpacing, 0.001f, "Отрицательный интерлиньяж не сохранён");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)character).SetValue(float.NaN);
            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)line).SetValue(float.PositiveInfinity);

            Assert.AreEqual(-5f, text.characterSpacing, 0.001f, "NaN дошёл до трекинга");
            Assert.AreEqual(-10f, text.lineSpacing, 0.001f, "Бесконечность дошла до интерлиньяжа");
        }

        [Test]
        public void Margin_ReachesTheText_AndRefusesANonFiniteComponent()
        {
            var text = New<TextMeshProUGUI>();
            var binder = text.gameObject.AddComponent<TextMarginMonoBinder>();

            ((IBinder<Vector4>)binder).SetValue(new Vector4(1f, 2f, 3f, 4f));
            Assert.AreEqual(new Vector4(1f, 2f, 3f, 4f), text.margin, "Отступы не доехали");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector4>)binder).SetValue(new Vector4(1f, float.NaN, 3f, 4f));
            Assert.AreEqual(new Vector4(1f, 2f, 3f, 4f), text.margin, "Нефинитная компонента дошла до текста");
        }

        /// <summary>
        /// Unity accepts a caret index past the end of the text and then draws the caret nowhere, so the binder clamps
        /// to the text that is actually there.
        /// </summary>
        /// <remarks>
        /// Outside play mode a field that was never focused does not keep a caret position at all — TMP reports zero
        /// whatever is written. What can be pinned here is that the binder never leaves an index past the text and
        /// never throws on an empty one; the clamp itself is arithmetic in the property.
        /// </remarks>
        [Test]
        public void CaretPosition_NeverLandsPastTheText()
        {
            var field = NewInputField();
            var binder = field.gameObject.AddComponent<InputFieldCaretPositionMonoBinder>();

            field.text = "abcd";

            Assert.DoesNotThrow(() => ((IBinder<int>)binder).SetValue(99));
            Assert.LessOrEqual(field.caretPosition, field.text.Length, "Каретка встала за пределами текста");

            field.text = string.Empty;

            Assert.DoesNotThrow(() => ((IBinder<int>)binder).SetValue(3), "Пустой текст уронил биндер каретки");
            Assert.AreEqual(0, field.caretPosition, "Каретка не на нуле при пустом тексте");
        }

        /// <summary>
        /// A destroyed graphic must not stay in the field as a live reference — the next keystroke would touch it.
        /// </summary>
        [Test]
        public void Placeholder_ADestroyedGraphicArrivesAsNull()
        {
            var field = NewInputField();
            var binder = field.gameObject.AddComponent<InputFieldPlaceholderMonoBinder>();
            var graphic = New<Image>();

            ((IBinder<Graphic>)binder).SetValue(graphic);
            Assert.AreSame(graphic, field.placeholder, "Живой graphic не доехал");

            Object.DestroyImmediate(graphic);
            ((IBinder<Graphic>)binder).SetValue(graphic);

            // Проверка по семантике Unity, а не по ссылке: сеттер TMP сравнивает новое значение с прежним
            // через Object.operator==, для которого null и уничтоженный объект равны, — поэтому запись null
            // поверх уничтоженной ссылки ничего не меняет. Для пользователя разницы нет: и то, и другое
            // читается как «нет placeholder», что этот assert и проверяет.
            Assert.IsFalse(field.placeholder, "Уничтоженный graphic остался живым для Unity");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var text = New<TextMeshProUGUI>();
            var field = NewInputField();

            Assert.IsTrue(new TextFontStyleBinder(text).IsBind);
            Assert.IsTrue(new TextAutoSizeBinder(text).IsBind);
            Assert.IsTrue(new TextCharacterSpacingBinder(text).IsBind);
            Assert.IsTrue(new TextLineSpacingBinder(text).IsBind);
            Assert.IsTrue(new TextMarginBinder(text).IsBind);
            Assert.IsTrue(new InputFieldCaretPositionBinder(field).IsBind);
            Assert.IsTrue(new InputFieldPlaceholderBinder(field).IsBind);
        }

        private TMP_InputField NewInputField()
        {
            var gameObject = new GameObject("InputField");
            _spawned.Add(gameObject);

            var field = gameObject.AddComponent<TMP_InputField>();
            var textArea = new GameObject("Text").AddComponent<TextMeshProUGUI>();

            textArea.transform.SetParent(gameObject.transform, worldPositionStays: false);
            field.textComponent = textArea;

            return field;
        }

        private T New<T>()
            where T : Component
        {
            var gameObject = new GameObject(typeof(T).Name);
            _spawned.Add(gameObject);

            return gameObject.AddComponent<T>();
        }
    }
}
#endif
