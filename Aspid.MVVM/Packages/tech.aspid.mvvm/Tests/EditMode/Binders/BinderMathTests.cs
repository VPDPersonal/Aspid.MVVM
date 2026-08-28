using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="BinderMath"/> and the Unity behaviour that makes it necessary.
    /// </summary>
    /// <remarks>
    /// The split the fixture pins: a finite value outside the range saturates at the bound in silence, because
    /// that is the documented contract; a non-finite one has no bound to saturate at, so it is replaced and
    /// reported.
    /// </remarks>
    [TestFixture]
    public sealed class BinderMathTests
    {
        private static readonly Regex NotFinite = new("is not finite");

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
        public void SafeClamp01_MapsNaNToZero_AndReportsIt()
        {
            LogAssert.Expect(LogType.Error, NotFinite);

            Assert.AreEqual(0f, BinderMath.SafeClamp01(typeof(BinderMathTests), float.NaN));
        }

        [Test]
        public void SafeClamp01_MapsInfinitiesToZero_AndReportsThem()
        {
            LogAssert.Expect(LogType.Error, NotFinite);
            LogAssert.Expect(LogType.Error, NotFinite);

            Assert.AreEqual(0f, BinderMath.SafeClamp01(typeof(BinderMathTests), float.PositiveInfinity));
            Assert.AreEqual(0f, BinderMath.SafeClamp01(typeof(BinderMathTests), float.NegativeInfinity));
        }

        /// <summary>
        /// A value outside the range is not a failure — it saturates and stays out of the console.
        /// </summary>
        [Test]
        public void SafeClamp01_ClampsFiniteValuesSilently()
        {
            Assert.AreEqual(0f, BinderMath.SafeClamp01(typeof(BinderMathTests), -1f));
            Assert.AreEqual(1f, BinderMath.SafeClamp01(typeof(BinderMathTests), 2f));
            Assert.AreEqual(0.5f, BinderMath.SafeClamp01(typeof(BinderMathTests), 0.5f));

            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void SafeClamp_MapsNonFiniteToTheLowerBound_AndReportsIt()
        {
            LogAssert.Expect(LogType.Error, NotFinite);
            LogAssert.Expect(LogType.Error, NotFinite);

            Assert.AreEqual(-3f, BinderMath.SafeClamp(typeof(BinderMathTests), float.NaN, -3f, 3f));
            Assert.AreEqual(-3f, BinderMath.SafeClamp(typeof(BinderMathTests), float.PositiveInfinity, -3f, 3f));
        }

        [Test]
        public void SafeClamp_ClampsFiniteValuesSilently()
        {
            Assert.AreEqual(-3f, BinderMath.SafeClamp(typeof(BinderMathTests), -10f, -3f, 3f));
            Assert.AreEqual(3f, BinderMath.SafeClamp(typeof(BinderMathTests), 10f, -3f, 3f));
            Assert.AreEqual(1.5f, BinderMath.SafeClamp(typeof(BinderMathTests), 1.5f, -3f, 3f));

            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void NonNegative_RaisesNegativesSilently_AndReportsNonFinite()
        {
            Assert.AreEqual(0f, BinderMath.NonNegative(typeof(BinderMathTests), -4f));
            Assert.AreEqual(4f, BinderMath.NonNegative(typeof(BinderMathTests), 4f));

            LogAssert.Expect(LogType.Error, NotFinite);
            Assert.AreEqual(0f, BinderMath.NonNegative(typeof(BinderMathTests), float.NaN));
        }

        /// <summary>
        /// A vector is reported once, not once per component.
        /// </summary>
        [Test]
        public void NonNegative_ReportsAVectorOnce()
        {
            LogAssert.Expect(LogType.Error, NotFinite);

            var result = BinderMath.NonNegative(typeof(BinderMathTests), new Vector2(float.NaN, float.NaN));

            Assert.AreEqual(Vector2.zero, result);
        }

        [Test]
        public void RequireFinite_RefusesAndReportsANonFiniteValue()
        {
            LogAssert.Expect(LogType.Error, NotFinite);

            Assert.IsFalse(BinderMath.RequireFinite(typeof(BinderMathTests), float.NaN));
        }

        [Test]
        public void RequireFinite_AcceptsAFiniteValueSilently()
        {
            Assert.IsTrue(BinderMath.RequireFinite(typeof(BinderMathTests), 12f));
            Assert.IsTrue(BinderMath.RequireFinite(typeof(BinderMathTests), new Vector2(1f, 2f)));
            Assert.IsTrue(BinderMath.RequireFinite(typeof(BinderMathTests), new Rect(0f, 0f, 1f, 1f)));

            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void RequireFinite_ReportsAVectorOnce()
        {
            LogAssert.Expect(LogType.Error, NotFinite);

            Assert.IsFalse(BinderMath.RequireFinite(typeof(BinderMathTests), new Vector3(float.NaN, float.NaN, 0f)));
        }

        /// <summary>
        /// End-to-end: a NaN arriving from the ViewModel must not reach the component, and must not do so quietly.
        /// </summary>
        [Test]
        public void CanvasGroupAlphaBinder_WithNaN_LeavesTheComponentFinite()
        {
            var gameObject = new GameObject("Alpha");
            _spawned.Add(gameObject);

            var canvasGroup = gameObject.AddComponent<CanvasGroup>();
            var binder = gameObject.AddComponent<CanvasGroupAlphaMonoBinder>();

            LogAssert.Expect(LogType.Error, NotFinite);
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

            LogAssert.Expect(LogType.Error, NotFinite);
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
