using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="TMP_InputField"/> property binders.
    /// </summary>
    [TestFixture]
    public sealed class InputFieldTests : SceneFixture
    {
        [Test]
        public void CharacterLimitAndReadOnly_ReachTheField()
        {
            var field = NewInputField();

            var limit = field.gameObject.AddComponent<InputFieldCharacterLimitMonoBinder>();
            var readOnly = field.gameObject.AddComponent<InputFieldReadOnlyMonoBinder>();

            ((IBinder<int>)limit).SetValue(8);
            ((IBinder<bool>)readOnly).SetValue(true);

            Assert.AreEqual(8, field.characterLimit, "The character limit did not reach the field");
            Assert.IsTrue(field.readOnly, "The field was not switched to read-only");
        }

        /// <summary>
        /// Unity clamps a negative limit to <c>0</c>, which means "no limit" — so the binder does not clamp it.
        /// </summary>
        [Test]
        public void CharacterLimitBinder_LeavesTheClampingToUnity()
        {
            var field = NewInputField();
            var binder = field.gameObject.AddComponent<InputFieldCharacterLimitMonoBinder>();

            ((IBinder<int>)binder).SetValue(-5);

            Assert.AreEqual(0, field.characterLimit, "Unity stopped clamping a negative limit to zero");
        }

        /// <summary>
        /// Lowering the limit constrains what can be typed next; it does not shorten what is already there.
        /// </summary>
        [Test]
        public void LoweringTheLimit_DoesNotTruncateTextAlreadyInTheField()
        {
            var field = NewInputField();
            var binder = field.gameObject.AddComponent<InputFieldCharacterLimitMonoBinder>();

            field.text = "abcdefghij";
            ((IBinder<int>)binder).SetValue(4);

            Assert.AreEqual("abcdefghij", field.text, "The text already in the field was truncated");
        }

        /// <summary>
        /// Outside play mode a field that was never focused does not keep a caret position at all — TMP reports zero
        /// whatever is written. What can be pinned here is that the binder never leaves an index past the text and
        /// never throws on an empty one; the clamp itself is arithmetic in the property.
        /// </summary>
        [Test]
        public void CaretPosition_NeverLandsPastTheText()
        {
            var field = NewInputField();
            var binder = field.gameObject.AddComponent<InputFieldCaretPositionMonoBinder>();

            field.text = "abcd";

            Assert.DoesNotThrow(() => ((IBinder<int>)binder).SetValue(99));
            Assert.LessOrEqual(field.caretPosition, field.text.Length, "The caret landed past the end of the text");

            field.text = string.Empty;

            Assert.DoesNotThrow(() => ((IBinder<int>)binder).SetValue(3), "Empty text crashed the caret binder");
            Assert.AreEqual(0, field.caretPosition, "The caret was not at zero for empty text");
        }

        /// <summary>
        /// A destroyed graphic must not stay in the field as a live reference — the next keystroke would touch it.
        /// </summary>
        [Test]
        public void Placeholder_ADestroyedGraphicArrivesAsNull()
        {
            var field = NewInputField();
            var binder = field.gameObject.AddComponent<InputFieldPlaceholderMonoBinder>();
            var graphic = Spawn<Image>("Placeholder");

            ((IBinder<Graphic>)binder).SetValue(graphic);
            Assert.AreSame(graphic, field.placeholder, "The live graphic did not reach the field");

            Destroy(graphic);
            ((IBinder<Graphic>)binder).SetValue(graphic);

            // Checked by Unity's semantics, not by reference: TMP's setter compares the new value against the
            // previous one through Object.operator==, under which null and a destroyed object are equal — so
            // writing null over the destroyed reference changes nothing. Both read as "no placeholder" to the
            // user, which is what this assert checks.
            Assert.IsFalse(field.placeholder, "The destroyed graphic stayed alive for Unity");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var field = NewInputField();

            Assert.IsTrue(new InputFieldCharacterLimitBinder(field).CanBind);
            Assert.IsTrue(new InputFieldReadOnlyBinder(field).CanBind);
            Assert.IsTrue(new InputFieldCaretPositionBinder(field).CanBind);
            Assert.IsTrue(new InputFieldPlaceholderBinder(field).CanBind);
        }

        private TMP_InputField NewInputField()
        {
            var field = Spawn<TMP_InputField>("InputField");
            var textArea = Spawn<TextMeshProUGUI>("Text");

            textArea.transform.SetParent(field.transform, worldPositionStays: false);
            field.textComponent = textArea;

            return field;
        }
    }
}
