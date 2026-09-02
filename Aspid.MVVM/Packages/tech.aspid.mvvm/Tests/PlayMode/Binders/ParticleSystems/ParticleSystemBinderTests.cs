using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new <see cref="ParticleSystem"/> binders.
    /// </summary>
    /// <remarks>
    /// These run in play mode because a particle system does not simulate in the editor, so
    /// <see cref="ParticleSystem.isPlaying"/> only means anything while the game runs.
    /// </remarks>
    [TestFixture]
    public sealed class ParticleSystemBinderTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator PlayBinder_StartsTheSystem()
        {
            var (system, binder) = Create<ParticleSystemPlayMonoBinder>();
            yield return null;

            Invoke(binder);

            Assert.IsTrue(system.isPlaying, "The system did not start");
        }

        [UnityTest]
        public IEnumerator StopBinder_StopsTheSystem()
        {
            var (system, binder) = Create<ParticleSystemStopMonoBinder>();
            system.Play();
            yield return null;

            Invoke(binder);

            Assert.IsFalse(system.isPlaying, "The system did not stop");
        }

        [UnityTest]
        public IEnumerator PauseBinder_PausesTheSystem()
        {
            var (system, binder) = Create<ParticleSystemPauseMonoBinder>();
            system.Play();
            yield return null;

            Invoke(binder);

            Assert.IsTrue(system.isPaused, "The system was not paused");
        }

        /// <summary>
        /// The command mirrors whether the operation can run, so a ViewModel can grey a button out rather than
        /// call into a system whose object is switched off.
        /// </summary>
        [UnityTest]
        public IEnumerator OnAnInactiveObject_TheCommandRefuses()
        {
            var (system, binder) = Create<ParticleSystemPlayMonoBinder>();
            yield return null;

            binder.gameObject.SetActive(false);

            IRelayCommand received = null;
            ((IBinder)binder).Bind(new OneWayToSourceBindableMember<IRelayCommand>(value => received = value));

            Assert.IsNotNull(received, "The command did not reach the ViewModel");
            Assert.IsFalse(received.CanExecute(), "The command agreed to run on a disabled object");

            received.Execute();
            Assert.IsFalse(system.isPlaying, "The system started on a disabled object");
        }

        /// <summary>
        /// A module is a struct holding a handle to the system rather than a copy of its data, so a write through
        /// a local reaches the system — this pins that, since the opposite would fail silently.
        /// </summary>
        [UnityTest]
        public IEnumerator EmissionBinder_TurnsEmissionOffWithoutStoppingTheSystem()
        {
            var gameObject = NewParticleObject(out var system);
            var binder = gameObject.AddComponent<ParticleSystemEmissionEnabledMonoBinder>();

            system.Play();
            yield return null;

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(system.emission.enabled, "Emission was not turned off");
            Assert.IsTrue(system.isPlaying, "The system stopped instead of continuing to play");
        }

        [UnityTest]
        public IEnumerator TheSerializableTwin_AcceptsItsTarget()
        {
            NewParticleObject(out var system);
            yield return null;

            Assert.IsTrue(new ParticleSystemEmissionEnabledBinder(system).CanBind);
        }

        private static void Invoke(ParticleSystemPlaybackMonoBinder binder)
        {
            Action received = null;
            ((IBinder)binder).Bind(new OneWayToSourceBindableMember<Action>(value => received = value));

            Assert.IsNotNull(received, "The binder did not hand an action to the ViewModel");
            received.Invoke();
        }

        private (ParticleSystem System, TBinder Binder) Create<TBinder>()
            where TBinder : ParticleSystemPlaybackMonoBinder
        {
            var gameObject = NewParticleObject(out var system);

            return (system, gameObject.AddComponent<TBinder>());
        }

        private GameObject NewParticleObject(out ParticleSystem system)
        {
            var gameObject = Spawn("Particles");

            system = gameObject.AddComponent<ParticleSystem>();
            system.Stop();

            return gameObject;
        }
    }
}
