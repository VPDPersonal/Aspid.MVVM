#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new <see cref="TMP_Text"/> and <see cref="TMP_InputField"/> binders.
    /// </summary>
    /// <remarks>
    /// Nothing here is sanitised, and a probe is why: Unity maps a negative character limit to <c>0</c> on its
    /// own, and neither property has a non-finite case to worry about. What the probe did turn up is that
    /// lowering the limit leaves text already in the field untouched — pinned below, because it is the kind of
    /// thing a binder makes easy to trip over.
    /// </remarks>
    [TestFixture]
    public sealed class TextBinderTests
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
        public void TextBinders_ReachTheLabel()
        {
            var gameObject = NewGameObject();
            var text = gameObject.AddComponent<TextMeshProUGUI>();
            text.text = "Hello";

            var visible = gameObject.AddComponent<TextMaxVisibleCharactersMonoBinder>();
            var rich = gameObject.AddComponent<TextRichTextMonoBinder>();

            ((IBinder<int>)visible).SetValue(3);
            ((IBinder<bool>)rich).SetValue(false);

            Assert.AreEqual(3, text.maxVisibleCharacters, "Число видимых символов не доехало");
            Assert.IsFalse(text.richText, "Разметка не отключена");
        }

        [Test]
        public void InputFieldBinders_ReachTheField()
        {
            var gameObject = NewGameObject();
            var field = gameObject.AddComponent<TMP_InputField>();

            var limit = gameObject.AddComponent<InputFieldCharacterLimitMonoBinder>();
            var readOnly = gameObject.AddComponent<InputFieldReadOnlyMonoBinder>();

            ((IBinder<int>)limit).SetValue(8);
            ((IBinder<bool>)readOnly).SetValue(true);

            Assert.AreEqual(8, field.characterLimit, "Лимит символов не доехал");
            Assert.IsTrue(field.readOnly, "Поле не переведено в режим только для чтения");
        }

        /// <summary>
        /// Unity clamps a negative limit to <c>0</c>, which means "no limit" — so the binder does not clamp it.
        /// </summary>
        [Test]
        public void CharacterLimitBinder_LeavesTheClampingToUnity()
        {
            var gameObject = NewGameObject();
            var field = gameObject.AddComponent<TMP_InputField>();
            var binder = gameObject.AddComponent<InputFieldCharacterLimitMonoBinder>();

            ((IBinder<int>)binder).SetValue(-5);

            Assert.AreEqual(0, field.characterLimit, "Unity перестала приводить отрицательный лимит к нулю");
        }

        /// <summary>
        /// Lowering the limit constrains what can be typed next; it does not shorten what is already there.
        /// </summary>
        [Test]
        public void LoweringTheLimit_DoesNotTruncateTextAlreadyInTheField()
        {
            var gameObject = NewGameObject();
            var field = gameObject.AddComponent<TMP_InputField>();
            var binder = gameObject.AddComponent<InputFieldCharacterLimitMonoBinder>();

            field.text = "abcdefghij";
            ((IBinder<int>)binder).SetValue(4);

            Assert.AreEqual("abcdefghij", field.text, "Уже введённый текст оказался обрезан");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var text = NewGameObject().AddComponent<TextMeshProUGUI>();
            var field = NewGameObject().AddComponent<TMP_InputField>();

            Assert.IsTrue(new TextMaxVisibleCharactersBinder(text).IsBind);
            Assert.IsTrue(new TextRichTextBinder(text).IsBind);
            Assert.IsTrue(new InputFieldCharacterLimitBinder(field).IsBind);
            Assert.IsTrue(new InputFieldReadOnlyBinder(field).IsBind);
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Text");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
#endif
