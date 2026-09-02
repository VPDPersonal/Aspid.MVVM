using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="ParticleSystem"/> module property binders: the emission rate and the start colour.
    /// </summary>
    [TestFixture]
    public sealed class ParticleSystemModulesBinderTests : SceneFixture
    {
        [Test]
        public void EmissionRate_ReachesTheModule()
        {
            var particles = Spawn<ParticleSystem>("Particles");
            var binder = particles.gameObject.AddComponent<ParticleSystemEmissionRateMonoBinder>();

            ((IBinder<float>)binder).SetValue(42f);

            Assert.AreEqual(42f, particles.emission.rateOverTimeMultiplier, 0.001f, "The emission rate did not reach the module");
        }

        [Test]
        public void EmissionRate_NegativeAndNonFinite_AreClampedToZero()
        {
            var particles = Spawn<ParticleSystem>("Particles");
            var binder = particles.gameObject.AddComponent<ParticleSystemEmissionRateMonoBinder>();

            ((IBinder<float>)binder).SetValue(-5f);
            Assert.AreEqual(0f, particles.emission.rateOverTimeMultiplier, 0.001f, "A negative rate was not clamped");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(particles.emission.rateOverTimeMultiplier), "NaN reached the module");
        }

        [Test]
        public void StartColor_ReachesTheModule()
        {
            var particles = Spawn<ParticleSystem>("Particles");
            var binder = particles.gameObject.AddComponent<ParticleSystemStartColorMonoBinder>();

            ((IBinder<Color>)binder).SetValue(Color.red);

            Assert.AreEqual(Color.red, particles.main.startColor.color, "The start colour did not reach the module");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var particles = Spawn<ParticleSystem>("Particles");

            Assert.IsTrue(new ParticleSystemEmissionRateBinder(particles).CanBind);
            Assert.IsTrue(new ParticleSystemStartColorBinder(particles).CanBind);
        }
    }
}
