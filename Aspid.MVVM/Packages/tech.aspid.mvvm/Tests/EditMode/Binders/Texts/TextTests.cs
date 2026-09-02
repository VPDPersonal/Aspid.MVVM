#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="TMP_Text"/> property binders.
    /// </summary>
    [TestFixture]
    public sealed class TextTests : SceneFixture
    {
        [Test]
        public void VisibleCharactersAndRichText_ReachTheLabel()
        {
            var text = Spawn<TextMeshProUGUI>("Text");
            text.text = "Hello";

            var visible = text.gameObject.AddComponent<TextMaxVisibleCharactersMonoBinder>();
            var rich = text.gameObject.AddComponent<TextRichTextMonoBinder>();

            ((IBinder<int>)visible).SetValue(3);
            ((IBinder<bool>)rich).SetValue(false);

            Assert.AreEqual(3, text.maxVisibleCharacters, "The visible character count did not reach the label");
            Assert.IsFalse(text.richText, "Rich text was not disabled");
        }

        [Test]
        public void FontStyle_TravelsAsTheWholeFlagSet()
        {
            var text = Spawn<TextMeshProUGUI>("Text");
            var binder = text.gameObject.AddComponent<TextFontStyleMonoBinder>();

            ((IBinder<FontStyles>)binder).SetValue(FontStyles.Bold | FontStyles.Italic);

            Assert.AreEqual(FontStyles.Bold | FontStyles.Italic, text.fontStyle, "The style flag set did not reach the text");
        }

        [Test]
        public void AutoSize_ReachesTheText()
        {
            var text = Spawn<TextMeshProUGUI>("Text");
            var binder = text.gameObject.AddComponent<TextAutoSizeMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);

            Assert.IsTrue(text.enableAutoSizing, "Auto size did not reach the text");
        }

        /// <summary>
        /// Negative tracking and leading are ordinary — that is how a title is tightened — so only a non-finite value
        /// is refused. TMP rebuilds its mesh from these numbers, and one <c>NaN</c> makes the text disappear.
        /// </summary>
        [Test]
        public void TheSpacings_KeepNegativesAndRefuseNonFinite()
        {
            var text = Spawn<TextMeshProUGUI>("Text");
            var character = text.gameObject.AddComponent<TextCharacterSpacingMonoBinder>();
            var line = text.gameObject.AddComponent<TextLineSpacingMonoBinder>();

            ((IBinder<float>)character).SetValue(-5f);
            ((IBinder<float>)line).SetValue(-10f);

            Assert.AreEqual(-5f, text.characterSpacing, 0.001f, "The negative tracking was not kept");
            Assert.AreEqual(-10f, text.lineSpacing, 0.001f, "The negative leading was not kept");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)character).SetValue(float.NaN);
            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)line).SetValue(float.PositiveInfinity);

            Assert.AreEqual(-5f, text.characterSpacing, 0.001f, "NaN reached the tracking");
            Assert.AreEqual(-10f, text.lineSpacing, 0.001f, "Infinity reached the leading");
        }

        [Test]
        public void Margin_ReachesTheText_AndRefusesANonFiniteComponent()
        {
            var text = Spawn<TextMeshProUGUI>("Text");
            var binder = text.gameObject.AddComponent<TextMarginMonoBinder>();

            ((IBinder<Vector4>)binder).SetValue(new Vector4(1f, 2f, 3f, 4f));
            Assert.AreEqual(new Vector4(1f, 2f, 3f, 4f), text.margin, "The margin did not reach the text");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector4>)binder).SetValue(new Vector4(1f, float.NaN, 3f, 4f));
            Assert.AreEqual(new Vector4(1f, 2f, 3f, 4f), text.margin, "A non-finite component reached the text");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var text = Spawn<TextMeshProUGUI>("Text");

            Assert.IsTrue(new TextMaxVisibleCharactersBinder(text).CanBind);
            Assert.IsTrue(new TextRichTextBinder(text).CanBind);
            Assert.IsTrue(new TextFontStyleBinder(text).CanBind);
            Assert.IsTrue(new TextAutoSizeBinder(text).CanBind);
            Assert.IsTrue(new TextCharacterSpacingBinder(text).CanBind);
            Assert.IsTrue(new TextLineSpacingBinder(text).CanBind);
            Assert.IsTrue(new TextMarginBinder(text).CanBind);
        }
    }
}
#endif
