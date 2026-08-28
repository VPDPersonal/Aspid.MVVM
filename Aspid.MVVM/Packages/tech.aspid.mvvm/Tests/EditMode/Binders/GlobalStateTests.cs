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
    /// Tests for the binders of state that belongs to no component: <see cref="Time.timeScale"/>, the quality level,
    /// the frame cap and fullscreen — plus the small UI properties that had no binder either.
    /// </summary>
    /// <remarks>
    /// Pause, slow motion and a settings screen's graphics options each needed a MonoBehaviour written for the purpose,
    /// because the value they change is global and the framework only bound components.
    /// </remarks>
    [TestFixture]
    public sealed class GlobalStateTests
    {
        private readonly List<GameObject> _spawned = new();

        private float _timeScale;
        private int _frameRate;

        [SetUp]
        public void SetUp()
        {
            _timeScale = Time.timeScale;
            _frameRate = Application.targetFrameRate;
        }

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = _timeScale;
            Application.targetFrameRate = _frameRate;

            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
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
            Assert.AreEqual(0.25f, Time.timeScale, 0.001f, "Масштаб времени не доехал");

            ((IBinder<float>)binder).SetValue(-2f);
            Assert.AreEqual(0f, Time.timeScale, 0.001f, "Отрицательный масштаб времени не обрезан");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(Time.timeScale), "NaN дошёл до масштаба времени");
        }

        [Test]
        public void TimeScale_OneWayToSource_ReportsTheCurrentScale()
        {
            Time.timeScale = 0.5f;

            var binder = new TimeScaleBinder(mode: BindMode.OneWayToSource);
            var received = float.NaN;

            binder.Bind(new OneWayToSourceStructBindableMember<float>(value => received = value));

            Assert.AreEqual(0.5f, received, 0.001f, "ViewModel не получила текущий масштаб времени");
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
            Assert.LessOrEqual(QualitySettings.GetQualityLevel(), levels - 1, "Уровень качества вышел за пределы списка");

            Assert.DoesNotThrow(() => ((IBinder<int>)binder).SetValue(-5));
            Assert.GreaterOrEqual(QualitySettings.GetQualityLevel(), 0, "Уровень качества стал отрицательным");
        }

        /// <summary>
        /// <c>-1</c> hands the decision back to the platform and is the only negative value that means anything.
        /// </summary>
        [Test]
        public void TargetFrameRate_ClampsEveryNegativeValueToMinusOne()
        {
            var binder = NewBinder<TargetFrameRateMonoBinder>();

            ((IBinder<int>)binder).SetValue(60);
            Assert.AreEqual(60, Application.targetFrameRate, "Ограничение кадров не доехало");

            ((IBinder<int>)binder).SetValue(-7);
            Assert.AreEqual(-1, Application.targetFrameRate, "Отрицательное значение не приведено к -1");
        }

        [Test]
        public void FullScreen_AcceptsAValueWithoutThrowing()
        {
            var binder = NewBinder<ScreenFullScreenMonoBinder>();

            Assert.DoesNotThrow(() => ((IBinder<bool>)binder).SetValue(Screen.fullScreen), "Биндер fullScreen упал");
        }

        [Test]
        public void Name_ReachesTheObject_AndRefusesNull()
        {
            var gameObject = NewGameObject();
            var binder = gameObject.AddComponent<GameObjectNameMonoBinder>();

            ((IBinder<string>)binder).SetValue("Slot 3");
            Assert.AreEqual("Slot 3", gameObject.name, "Имя не доехало");

            ((IBinder<string>)binder).SetValue(null);
            Assert.AreEqual("Slot 3", gameObject.name, "Null стёр имя объекта");
        }

        [Test]
        public void TheShadowBinders_ReachTheEffect()
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<Image>();

            var shadow = gameObject.AddComponent<Outline>();
            var color = gameObject.AddComponent<ShadowEffectColorMonoBinder>();
            var distance = gameObject.AddComponent<ShadowEffectDistanceMonoBinder>();

            ((IBinder<Color>)color).SetValue(Color.red);
            ((IBinder<Vector2>)distance).SetValue(new Vector2(-2f, 3f));

            Assert.AreEqual(Color.red, shadow.effectColor, "Цвет эффекта не доехал");
            Assert.AreEqual(new Vector2(-2f, 3f), shadow.effectDistance, "Смещение эффекта не доехало");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)distance).SetValue(new Vector2(float.NaN, 0f));
            Assert.AreEqual(new Vector2(-2f, 3f), shadow.effectDistance, "Нефинитное смещение дошло до эффекта");
        }

        [Test]
        public void TheMaskBinder_ReachesTheMask()
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<Image>();

            var mask = gameObject.AddComponent<Mask>();
            var binder = gameObject.AddComponent<MaskShowMaskGraphicMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(mask.showMaskGraphic, "Показ графики маски не доехал");
        }

        [Test]
        public void TheSerializableTwins_Bind()
        {
            Assert.IsTrue(new TimeScaleBinder().IsBind);
            Assert.IsTrue(new QualityLevelBinder().IsBind);
            Assert.IsTrue(new TargetFrameRateBinder().IsBind);
            Assert.IsTrue(new ScreenFullScreenBinder().IsBind);
        }

        private T NewBinder<T>()
            where T : MonoBinder =>
            NewGameObject().AddComponent<T>();

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("GlobalState");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
