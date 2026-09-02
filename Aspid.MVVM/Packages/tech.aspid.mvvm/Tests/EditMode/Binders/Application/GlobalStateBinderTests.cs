using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the binders of state that belongs to no component: <see cref="Time.timeScale"/>, the quality level,
    /// the frame cap and fullscreen.
    /// </summary>
    [TestFixture]
    public sealed class GlobalStateBinderTests : SceneFixture
    {
        [SetUp]
        public void SetUp()
        {
            var timeScale = Time.timeScale;
            var frameRate = Application.targetFrameRate;

            RestoreOnTearDown(() =>
            {
                Time.timeScale = timeScale;
                Application.targetFrameRate = frameRate;
            });
        }

        /// <summary>
        /// Unity refuses a negative time scale and logs an error for it, so the binder clamps at zero — which pauses the
        /// game, the nearest sensible reading of "less than none".
        /// </summary>
        [Test]
        public void TimeScale_IsNeverNegative_AndNeverNonFinite()
        {
            var binder = NewBinder<TimeScaleMonoBinder>();

            ((IBinder<float>)binder).SetValue(0.25f);
            Assert.AreEqual(0.25f, Time.timeScale, 0.001f, "The time scale did not reach the engine");

            ((IBinder<float>)binder).SetValue(-2f);
            Assert.AreEqual(0f, Time.timeScale, 0.001f, "A negative time scale was not clamped");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(Time.timeScale), "NaN reached the time scale");
        }

        [Test]
        public void TimeScale_OneWayToSource_ReportsTheCurrentScale()
        {
            Time.timeScale = 0.5f;

            var binder = new TimeScaleBinder(mode: BindMode.OneWayToSource);
            var received = float.NaN;

            binder.Bind(new OneWayToSourceStructBindableMember<float>(value => received = value));

            Assert.AreEqual(0.5f, received, 0.001f, "The ViewModel did not receive the current time scale");
        }

        /// <summary>
        /// The quality level is an index into the levels the project defines, and Unity throws on one it does not have.
        /// </summary>
        [Test]
        public void QualityLevel_IsClampedToTheLevelsThatExist()
        {
            var binder = NewBinder<QualityLevelMonoBinder>();
            var levels = QualitySettings.names.Length;

            Assert.DoesNotThrow(() => ((IBinder<int>)binder).SetValue(999));
            Assert.LessOrEqual(QualitySettings.GetQualityLevel(), levels - 1, "The quality level went past the list");

            Assert.DoesNotThrow(() => ((IBinder<int>)binder).SetValue(-5));
            Assert.GreaterOrEqual(QualitySettings.GetQualityLevel(), 0, "The quality level went negative");
        }

        /// <summary>
        /// <c>-1</c> hands the decision back to the platform and is the only negative value that means anything.
        /// </summary>
        [Test]
        public void TargetFrameRate_ClampsEveryNegativeValueToMinusOne()
        {
            var binder = NewBinder<TargetFrameRateMonoBinder>();

            ((IBinder<int>)binder).SetValue(60);
            Assert.AreEqual(60, Application.targetFrameRate, "The frame cap did not reach the engine");

            ((IBinder<int>)binder).SetValue(-7);
            Assert.AreEqual(-1, Application.targetFrameRate, "A negative value was not mapped to -1");
        }

        [Test]
        public void FullScreen_AcceptsAValueWithoutThrowing()
        {
            var binder = NewBinder<ScreenFullScreenMonoBinder>();

            Assert.DoesNotThrow(() => ((IBinder<bool>)binder).SetValue(Screen.fullScreen), "The fullScreen binder threw");
        }

        [Test]
        public void TheSerializableTwins_Bind()
        {
            Assert.IsTrue(new TimeScaleBinder().CanBind);
            Assert.IsTrue(new QualityLevelBinder().CanBind);
            Assert.IsTrue(new TargetFrameRateBinder().CanBind);
            Assert.IsTrue(new ScreenFullScreenBinder().CanBind);
        }

        private T NewBinder<T>()
            where T : MonoBinder =>
            Spawn("GlobalState").AddComponent<T>();
    }
}
