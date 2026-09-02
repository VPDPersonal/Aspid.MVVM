using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Light"/> binders: colour, intensity, range and spot angle.
    /// </summary>
    /// <remarks>
    /// Unity clamps every range it cares about — intensity, spot angle — but stores <see cref="float.NaN"/>
    /// verbatim, and maps a non-finite <see cref="Light.range"/> to zero, which switches the lamp off. So the
    /// binders guard exactly one thing: they drop a non-finite write and clamp nothing themselves.
    /// </remarks>
    [TestFixture]
    public sealed class LightBinderTests : SceneFixture
    {
        [Test]
        public void LightBinders_ReachTheLamp()
        {
            var light = Spawn<Light>("Light");
            light.type = LightType.Spot;

            var color = light.gameObject.AddComponent<LightColorMonoBinder>();
            var intensity = light.gameObject.AddComponent<LightIntensityMonoBinder>();
            var range = light.gameObject.AddComponent<LightRangeMonoBinder>();
            var angle = light.gameObject.AddComponent<LightSpotAngleMonoBinder>();

            ((IBinder<Color>)color).SetValue(Color.red);
            ((IBinder<float>)intensity).SetValue(2.5f);
            ((IBinder<float>)range).SetValue(12f);
            ((IBinder<float>)angle).SetValue(45f);

            Assert.AreEqual(Color.red, light.color, "The colour did not reach the light");
            Assert.AreEqual(2.5f, light.intensity, 0.001f, "The intensity did not reach the light");
            Assert.AreEqual(12f, light.range, 0.001f, "The range did not reach the light");
            Assert.AreEqual(45f, light.spotAngle, 0.001f, "The spot angle did not reach the light");
        }

        /// <summary>
        /// Unity stores a NaN intensity verbatim, and a NaN range it turns into zero — which switches the lamp
        /// off. Both are dropped so the lamp keeps the last values that lit something.
        /// </summary>
        [Test]
        public void LightBinders_DropANonFiniteValue()
        {
            var light = Spawn<Light>("Light");

            var intensity = light.gameObject.AddComponent<LightIntensityMonoBinder>();
            var range = light.gameObject.AddComponent<LightRangeMonoBinder>();

            ((IBinder<float>)intensity).SetValue(2.5f);
            ((IBinder<float>)range).SetValue(12f);

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)intensity).SetValue(float.NaN);
            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)range).SetValue(float.NaN);

            Assert.AreEqual(2.5f, light.intensity, 0.001f, "NaN overwrote the intensity");
            Assert.AreEqual(12f, light.range, 0.001f, "NaN switched the lamp off");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var light = Spawn<Light>("Light");

            Assert.IsTrue(new LightColorBinder(light).CanBind);
            Assert.IsTrue(new LightIntensityBinder(light).CanBind);
            Assert.IsTrue(new LightRangeBinder(light).CanBind);
            Assert.IsTrue(new LightSpotAngleBinder(light).CanBind);
        }
    }
}
