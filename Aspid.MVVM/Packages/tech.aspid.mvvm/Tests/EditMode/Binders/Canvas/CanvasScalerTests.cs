using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="CanvasScaler"/> binders.
    /// </summary>
    [TestFixture]
    public sealed class CanvasScalerTests : SceneFixture
    {
        [Test]
        public void UiScaleMode_ReachesTheScaler()
        {
            var scaler = Spawn<CanvasScaler>("CanvasScaler");
            var binder = scaler.gameObject.AddComponent<CanvasScalerUiScaleModeMonoBinder>();

            ((IBinder<CanvasScaler.ScaleMode>)binder).SetValue(CanvasScaler.ScaleMode.ConstantPixelSize);

            Assert.AreEqual(CanvasScaler.ScaleMode.ConstantPixelSize, scaler.uiScaleMode, "The scale mode did not reach the scaler");
        }

        [Test]
        public void ScaleFactor_ReachesTheScaler_AndIsClamped()
        {
            var scaler = Spawn<CanvasScaler>("CanvasScaler");
            var binder = scaler.gameObject.AddComponent<CanvasScalerScaleFactorMonoBinder>();

            ((IBinder<float>)binder).SetValue(1.5f);
            Assert.AreEqual(1.5f, scaler.scaleFactor, 0.001f, "The scale factor did not reach the scaler");

            ((IBinder<float>)binder).SetValue(-3f);
            Assert.AreEqual(0.01f, scaler.scaleFactor, 0.001f, "A negative scale factor was not raised to Unity's minimum");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(scaler.scaleFactor), "NaN reached the scaler");
        }

        /// <summary>
        /// The scaler divides the screen size by this value, so zero would scale the canvas to infinity.
        /// </summary>
        [Test]
        public void ReferenceResolution_IsNeverBelowOne()
        {
            var scaler = Spawn<CanvasScaler>("CanvasScaler");
            var binder = scaler.gameObject.AddComponent<CanvasScalerReferenceResolutionMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(1920f, 1080f));
            Assert.AreEqual(new Vector2(1920f, 1080f), scaler.referenceResolution, "The resolution did not reach the scaler");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(0f, float.NaN));
            Assert.AreEqual(new Vector2(1f, 1f), scaler.referenceResolution, "A zero or non-finite resolution was not raised to one");
        }

        [Test]
        public void MatchWidthOrHeight_IsClampedToTheDocumentedRange()
        {
            var scaler = Spawn<CanvasScaler>("CanvasScaler");
            var binder = scaler.gameObject.AddComponent<CanvasScalerMatchWidthOrHeightMonoBinder>();

            ((IBinder<float>)binder).SetValue(2f);

            Assert.AreEqual(1f, scaler.matchWidthOrHeight, 0.001f, "A value outside 0..1 was not clamped");
        }

        /// <summary>
        /// None of the scaler's enum properties raise a change event, so a two-way channel would never deliver.
        /// </summary>
        [Test]
        public void UiScaleModeBinder_RefusesTwoWay()
        {
            var scaler = Spawn<CanvasScaler>("CanvasScaler");

            Assert.Throws<System.ArgumentException>(
                () => _ = new CanvasScalerUiScaleModeBinder(scaler, mode: BindMode.TwoWay),
                "TwoWay was accepted by a mode where no reverse channel is possible");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var scaler = Spawn<CanvasScaler>("CanvasScaler");

            Assert.IsTrue(new CanvasScalerUiScaleModeBinder(scaler).CanBind);
            Assert.IsTrue(new CanvasScalerScaleFactorBinder(scaler).CanBind);
            Assert.IsTrue(new CanvasScalerReferenceResolutionBinder(scaler).CanBind);
            Assert.IsTrue(new CanvasScalerMatchWidthOrHeightBinder(scaler).CanBind);
        }
    }
}
