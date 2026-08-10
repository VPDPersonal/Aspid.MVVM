using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="BinderMath"/> and the Unity behaviour that makes it necessary.
    /// </summary>
    [TestFixture]
    public sealed class BinderMathTests
    {
        /// <summary>
        /// Pins the premise rather than our own code: <see cref="Mathf.Clamp(float, float, float)"/> is two
        /// comparisons and both are false for NaN, so NaN survives a clamp untouched. If a future Unity version
        /// changes this, the helper stops being needed and this test says so.
        /// </summary>
        [Test]
        public void MathfClamp_LetsNaNThrough()
        {
            Assert.IsNaN(Mathf.Clamp(float.NaN, 0f, 1f));
            Assert.IsNaN(Mathf.Clamp01(float.NaN));
        }

        [Test]
        public void SafeClamp01_MapsNaNToZero() =>
            Assert.AreEqual(0f, BinderMath.SafeClamp01(float.NaN));

        [Test]
        public void SafeClamp01_MapsInfinitiesToZero()
        {
            Assert.AreEqual(0f, BinderMath.SafeClamp01(float.PositiveInfinity));
            Assert.AreEqual(0f, BinderMath.SafeClamp01(float.NegativeInfinity));
        }

        [Test]
        public void SafeClamp01_ClampsFiniteValuesAsBefore()
        {
            Assert.AreEqual(0f, BinderMath.SafeClamp01(-1f));
            Assert.AreEqual(1f, BinderMath.SafeClamp01(2f));
            Assert.AreEqual(0.5f, BinderMath.SafeClamp01(0.5f));
        }

        [Test]
        public void SafeClamp_MapsNonFiniteToTheLowerBound()
        {
            Assert.AreEqual(-3f, BinderMath.SafeClamp(float.NaN, -3f, 3f));
            Assert.AreEqual(-3f, BinderMath.SafeClamp(float.PositiveInfinity, -3f, 3f));
        }

        [Test]
        public void SafeClamp_ClampsFiniteValuesAsBefore()
        {
            Assert.AreEqual(-3f, BinderMath.SafeClamp(-10f, -3f, 3f));
            Assert.AreEqual(3f, BinderMath.SafeClamp(10f, -3f, 3f));
            Assert.AreEqual(1.5f, BinderMath.SafeClamp(1.5f, -3f, 3f));
        }
    
        /// <summary>
        /// End-to-end: a NaN arriving from the ViewModel must not reach the component.
        /// </summary>
        [Test]
        public void CanvasGroupAlphaBinder_WithNaN_LeavesTheComponentFinite()
        {
            var gameObject = new GameObject("Alpha");
            _spawned.Add(gameObject);

            var canvasGroup = gameObject.AddComponent<CanvasGroup>();
            var binder = gameObject.AddComponent<CanvasGroupAlphaMonoBinder>();

            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.IsTrue(BinderMath.IsFinite(canvasGroup.alpha), $"В компонент попало {canvasGroup.alpha}");
        }

        [Test]
        public void ImageFillBinder_WithNaN_LeavesTheComponentFinite()
        {
            var gameObject = new GameObject("Fill");
            _spawned.Add(gameObject);

            var image = gameObject.AddComponent<UnityEngine.UI.Image>();
            var binder = gameObject.AddComponent<ImageFillMonoBinder>();

            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.IsTrue(BinderMath.IsFinite(image.fillAmount), $"В компонент попало {image.fillAmount}");
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        private readonly System.Collections.Generic.List<GameObject> _spawned = new();
    }
}
