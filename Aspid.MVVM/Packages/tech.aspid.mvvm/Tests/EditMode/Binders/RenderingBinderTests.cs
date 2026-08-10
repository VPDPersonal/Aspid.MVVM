using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new <see cref="Light"/> and <see cref="Camera"/> binders.
    /// </summary>
    /// <remarks>
    /// Lighting and cameras had no binders at all. A probe against the raw components showed Unity clamps every
    /// range it cares about — intensity, spot angle, field of view — but stores <see cref="float.NaN"/> verbatim
    /// in all of them, and maps a non-finite <see cref="Light.range"/> to zero, which switches the lamp off. So
    /// the domain guards exactly one thing: it drops a non-finite write and clamps nothing.
    /// </remarks>
    [TestFixture]
    public sealed class RenderingBinderTests
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
        public void LightBinders_ReachTheLamp()
        {
            var gameObject = NewGameObject();
            var light = gameObject.AddComponent<Light>();
            light.type = LightType.Spot;

            var color = gameObject.AddComponent<LightColorMonoBinder>();
            var intensity = gameObject.AddComponent<LightIntensityMonoBinder>();
            var range = gameObject.AddComponent<LightRangeMonoBinder>();
            var angle = gameObject.AddComponent<LightSpotAngleMonoBinder>();

            ((IBinder<Color>)color).SetValue(Color.red);
            ((IBinder<float>)intensity).SetValue(2.5f);
            ((IBinder<float>)range).SetValue(12f);
            ((IBinder<float>)angle).SetValue(45f);

            Assert.AreEqual(Color.red, light.color, "Цвет не доехал");
            Assert.AreEqual(2.5f, light.intensity, 0.001f, "Яркость не доехала");
            Assert.AreEqual(12f, light.range, 0.001f, "Дальность не доехала");
            Assert.AreEqual(45f, light.spotAngle, 0.001f, "Угол конуса не доехал");
        }

        /// <summary>
        /// Unity stores a NaN intensity verbatim, and a NaN range it turns into zero — which switches the lamp
        /// off. Both are dropped so the lamp keeps the last values that lit something.
        /// </summary>
        [Test]
        public void LightBinders_DropANonFiniteValue()
        {
            var gameObject = NewGameObject();
            var light = gameObject.AddComponent<Light>();

            var intensity = gameObject.AddComponent<LightIntensityMonoBinder>();
            var range = gameObject.AddComponent<LightRangeMonoBinder>();

            ((IBinder<float>)intensity).SetValue(2.5f);
            ((IBinder<float>)range).SetValue(12f);

            ((IBinder<float>)intensity).SetValue(float.NaN);
            ((IBinder<float>)range).SetValue(float.NaN);

            Assert.AreEqual(2.5f, light.intensity, 0.001f, "NaN затёр яркость");
            Assert.AreEqual(12f, light.range, 0.001f, "NaN погасил лампу");
        }

        [Test]
        public void CameraBinders_ReachTheCamera()
        {
            var gameObject = NewGameObject();
            var camera = gameObject.AddComponent<Camera>();

            var fieldOfView = gameObject.AddComponent<CameraFieldOfViewMonoBinder>();
            var background = gameObject.AddComponent<CameraBackgroundColorMonoBinder>();
            var orthographic = gameObject.AddComponent<CameraOrthographicMonoBinder>();
            var size = gameObject.AddComponent<CameraOrthographicSizeMonoBinder>();

            ((IBinder<float>)fieldOfView).SetValue(75f);
            ((IBinder<Color>)background).SetValue(Color.green);
            ((IBinder<bool>)orthographic).SetValue(true);
            ((IBinder<float>)size).SetValue(8f);

            Assert.AreEqual(75f, camera.fieldOfView, 0.001f, "Поле зрения не доехало");
            Assert.AreEqual(Color.green, camera.backgroundColor, "Цвет фона не доехал");
            Assert.IsTrue(camera.orthographic, "Проекция не переключена");
            Assert.AreEqual(8f, camera.orthographicSize, 0.001f, "Ортографический размер не доехал");
        }

        [Test]
        public void CameraBinders_DropANonFiniteValue()
        {
            var gameObject = NewGameObject();
            var camera = gameObject.AddComponent<Camera>();

            var fieldOfView = gameObject.AddComponent<CameraFieldOfViewMonoBinder>();
            var size = gameObject.AddComponent<CameraOrthographicSizeMonoBinder>();

            ((IBinder<float>)fieldOfView).SetValue(75f);
            ((IBinder<float>)size).SetValue(8f);

            ((IBinder<float>)fieldOfView).SetValue(float.NaN);
            ((IBinder<float>)size).SetValue(float.PositiveInfinity);

            Assert.AreEqual(75f, camera.fieldOfView, 0.001f, "NaN затёр поле зрения");
            Assert.AreEqual(8f, camera.orthographicSize, 0.001f, "Бесконечность затёрла ортографический размер");
        }

        /// <summary>
        /// A negative orthographic size mirrors the view rather than being invalid, so it is passed through.
        /// </summary>
        [Test]
        public void OrthographicSizeBinder_KeepsANegativeValue()
        {
            var gameObject = NewGameObject();
            var camera = gameObject.AddComponent<Camera>();
            var binder = gameObject.AddComponent<CameraOrthographicSizeMonoBinder>();

            ((IBinder<float>)binder).SetValue(-4f);

            Assert.AreEqual(-4f, camera.orthographicSize, 0.001f, "Отрицательный размер был обрезан");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var light = NewGameObject().AddComponent<Light>();
            var camera = NewGameObject().AddComponent<Camera>();

            Assert.IsTrue(new LightColorBinder(light).IsBind);
            Assert.IsTrue(new LightIntensityBinder(light).IsBind);
            Assert.IsTrue(new LightRangeBinder(light).IsBind);
            Assert.IsTrue(new LightSpotAngleBinder(light).IsBind);
            Assert.IsTrue(new CameraFieldOfViewBinder(camera).IsBind);
            Assert.IsTrue(new CameraOrthographicSizeBinder(camera).IsBind);
            Assert.IsTrue(new CameraBackgroundColorBinder(camera).IsBind);
            Assert.IsTrue(new CameraOrthographicBinder(camera).IsBind);
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Rendering");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
