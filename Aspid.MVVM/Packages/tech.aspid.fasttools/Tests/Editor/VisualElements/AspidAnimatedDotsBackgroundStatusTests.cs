using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;

namespace Aspid.FastTools.UIElements.Editors.Internal.Tests
{
    /// <summary>
    /// Behavioural coverage for the dots canvas' status wash: a status class must repaint all three blobs one flat
    /// tone through USS, and dropping it must bring the default three-tone signal gradient back.
    /// </summary>
    /// <remarks>
    /// The round trip is the point. The wash used to be applied as inline colors, which latched the blobs out of USS
    /// resolution for good and forced the "restore the gradient" path to re-supply the palette values from C#.
    /// Asserted structurally (blobs equal / distinct, channel ordering) rather than against literal rgb values —
    /// pinning the numbers here would re-create exactly the palette duplication this covers.
    /// </remarks>
    [TestFixture]
    internal sealed class AspidAnimatedDotsBackgroundStatusTests
    {
        private EditorWindow _window;
        private AspidAnimatedDotsBackground _canvas;

        [SetUp]
        public void SetUp()
        {
            _window = ScriptableObject.CreateInstance<EditorWindow>();
            _window.ShowUtility();

            // The default gradient resolves through the shared palette, so the host needs the theme sheets — the
            // status washes themselves are component-scoped and come with the canvas' own stylesheet.
            _window.rootVisualElement.AddAspidThemeStyleSheets();

            _canvas = new AspidAnimatedDotsBackground();
            _window.rootVisualElement.Add(_canvas);
        }

        [TearDown]
        public void TearDown()
        {
            if (_window) Object.DestroyImmediate(_window);
        }

        [UnityTest]
        public IEnumerator None_ResolvesTheThreeToneSignalGradient()
        {
            yield return null;

            Assert.AreNotEqual(_canvas.Color1, _canvas.Color2, "The default canvas must keep its three distinct signal blobs.");
            Assert.AreNotEqual(_canvas.Color2, _canvas.Color3, "The default canvas must keep its three distinct signal blobs.");
        }

        [UnityTest]
        public IEnumerator Status_PaintsEveryBlobTheSameWash()
        {
            _canvas.Status = StatusStyle.Type.Warning;
            yield return null;

            Assert.AreEqual(_canvas.Color1, _canvas.Color2, "A status wash must paint every blob the one tone.");
            Assert.AreEqual(_canvas.Color2, _canvas.Color3, "A status wash must paint every blob the one tone.");

            var wash = _canvas.Color1;
            Assert.Greater(wash.r, wash.g, "The warning wash must read amber — red over green over blue.");
            Assert.Greater(wash.g, wash.b, "The warning wash must read amber — red over green over blue.");
        }

        [UnityTest]
        public IEnumerator Status_SwapsBetweenWashes()
        {
            _canvas.Status = StatusStyle.Type.Warning;
            yield return null;

            var warning = _canvas.Color1;

            _canvas.Status = StatusStyle.Type.Success;
            yield return null;

            Assert.AreNotEqual(warning, _canvas.Color1, "Switching status must repaint the wash.");
            Assert.Greater(_canvas.Color1.g, _canvas.Color1.r, "The success wash must read green.");
        }

        [UnityTest]
        public IEnumerator None_RestoresTheGradientAfterAWash()
        {
            _canvas.Status = StatusStyle.Type.Warning;
            yield return null;

            Assert.AreEqual(_canvas.Color1, _canvas.Color2, "Precondition: the wash is on.");

            _canvas.Status = StatusStyle.Type.None;
            yield return null;

            Assert.AreNotEqual(_canvas.Color1, _canvas.Color2, "Dropping the status must hand the blobs back to the USS gradient.");
            Assert.AreNotEqual(_canvas.Color2, _canvas.Color3, "Dropping the status must hand the blobs back to the USS gradient.");
        }
    }
}
