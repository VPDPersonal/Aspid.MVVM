using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Camera"/> binders: field of view, background colour, orthographic mode and size.
    /// </summary>
    /// <remarks>
    /// Unity clamps the field of view but stores <see cref="float.NaN"/> verbatim, so the binders guard exactly one
    /// thing: they drop a non-finite write and clamp nothing themselves.
    /// </remarks>
    [TestFixture]
    public sealed class CameraBinderTests : SceneFixture
    {
        [Test]
        public void CameraBinders_ReachTheCamera()
        {
            var camera = Spawn<Camera>("Camera");

            var fieldOfView = camera.gameObject.AddComponent<CameraFieldOfViewMonoBinder>();
            var background = camera.gameObject.AddComponent<CameraBackgroundColorMonoBinder>();
            var orthographic = camera.gameObject.AddComponent<CameraOrthographicMonoBinder>();
            var size = camera.gameObject.AddComponent<CameraOrthographicSizeMonoBinder>();

            ((IBinder<float>)fieldOfView).SetValue(75f);
            ((IBinder<Color>)background).SetValue(Color.green);
            ((IBinder<bool>)orthographic).SetValue(true);
            ((IBinder<float>)size).SetValue(8f);

            Assert.AreEqual(75f, camera.fieldOfView, 0.001f, "The field of view did not reach the camera");
            Assert.AreEqual(Color.green, camera.backgroundColor, "The background colour did not reach the camera");
            Assert.IsTrue(camera.orthographic, "The projection was not switched");
            Assert.AreEqual(8f, camera.orthographicSize, 0.001f, "The orthographic size did not reach the camera");
        }

        [Test]
        public void CameraBinders_DropANonFiniteValue()
        {
            var camera = Spawn<Camera>("Camera");

            var fieldOfView = camera.gameObject.AddComponent<CameraFieldOfViewMonoBinder>();
            var size = camera.gameObject.AddComponent<CameraOrthographicSizeMonoBinder>();

            ((IBinder<float>)fieldOfView).SetValue(75f);
            ((IBinder<float>)size).SetValue(8f);

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)fieldOfView).SetValue(float.NaN);
            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)size).SetValue(float.PositiveInfinity);

            Assert.AreEqual(75f, camera.fieldOfView, 0.001f, "NaN overwrote the field of view");
            Assert.AreEqual(8f, camera.orthographicSize, 0.001f, "Infinity overwrote the orthographic size");
        }

        /// <summary>
        /// A negative orthographic size mirrors the view rather than being invalid, so it is passed through.
        /// </summary>
        [Test]
        public void OrthographicSizeBinder_KeepsANegativeValue()
        {
            var camera = Spawn<Camera>("Camera");
            var binder = camera.gameObject.AddComponent<CameraOrthographicSizeMonoBinder>();

            ((IBinder<float>)binder).SetValue(-4f);

            Assert.AreEqual(-4f, camera.orthographicSize, 0.001f, "The negative size was clamped");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var camera = Spawn<Camera>("Camera");

            Assert.IsTrue(new CameraFieldOfViewBinder(camera).CanBind);
            Assert.IsTrue(new CameraOrthographicSizeBinder(camera).CanBind);
            Assert.IsTrue(new CameraBackgroundColorBinder(camera).CanBind);
            Assert.IsTrue(new CameraOrthographicBinder(camera).CanBind);
        }
    }
}
