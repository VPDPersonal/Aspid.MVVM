using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="AspectRatioFitter"/> binders.
    /// </summary>
    [TestFixture]
    public sealed class AspectRatioFitterTests : SceneFixture
    {
        [Test]
        public void AspectRatioFitter_ModeAndRatioReachTheFitter()
        {
            var fitter = Spawn<AspectRatioFitter>("AspectRatioFitter");
            var mode = fitter.gameObject.AddComponent<AspectRatioFitterAspectModeMonoBinder>();
            var ratio = fitter.gameObject.AddComponent<AspectRatioFitterAspectRatioMonoBinder>();

            ((IBinder<AspectRatioFitter.AspectMode>)mode).SetValue(AspectRatioFitter.AspectMode.WidthControlsHeight);
            ((IBinder<float>)ratio).SetValue(16f / 9f);

            Assert.AreEqual(AspectRatioFitter.AspectMode.WidthControlsHeight, fitter.aspectMode, "The mode did not reach the fitter");
            Assert.AreEqual(16f / 9f, fitter.aspectRatio, 0.001f, "The ratio did not reach the fitter");
        }

        /// <summary>
        /// Unity clamps the ratio with comparisons, and every comparison against <c>NaN</c> is false — so the
        /// binder has to refuse it before the clamp does not.
        /// </summary>
        [Test]
        public void AspectRatio_RefusesANonFiniteValue()
        {
            var fitter = Spawn<AspectRatioFitter>("AspectRatioFitter");
            // Outside play mode a fitter in AspectMode.None recomputes the ratio from the current rect on its
            // own, so a written value would not survive — that is documented on the binder itself.
            fitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;

            var binder = fitter.gameObject.AddComponent<AspectRatioFitterAspectRatioMonoBinder>();

            ((IBinder<float>)binder).SetValue(2f);
            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.AreEqual(2f, fitter.aspectRatio, 0.001f, "NaN reached the fitter");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var fitter = Spawn<AspectRatioFitter>("AspectRatioFitter");

            Assert.IsTrue(new AspectRatioFitterAspectModeBinder(fitter).CanBind);
            Assert.IsTrue(new AspectRatioFitterAspectRatioBinder(fitter).CanBind);
        }
    }
}
