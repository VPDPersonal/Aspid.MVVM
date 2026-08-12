#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new <see cref="ParticleSystem"/> binders.
    /// </summary>
    /// <remarks>
    /// An effect is something a ViewModel starts — a hit, a pickup, a level-up — and nothing in the package could
    /// start one. These run in play mode for the same reason the audio ones do: a particle system does not
    /// simulate in the editor, so <see cref="ParticleSystem.isPlaying"/> only means anything while the game runs.
    /// </remarks>
    [TestFixture]
    public sealed class ParticleSystemBinderTests
    {
        private readonly List<UnityEngine.Object> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var spawned in _spawned)
            {
                if (spawned) UnityEngine.Object.Destroy(spawned);
            }

            _spawned.Clear();
        }

        [UnityTest]
        public IEnumerator PlayBinder_StartsTheSystem()
        {
            var (system, binder) = Create<ParticleSystemPlayMonoBinder>();
            yield return null;

            Invoke(binder);

            Assert.IsTrue(system.isPlaying, "Система не запущена");
        }

        [UnityTest]
        public IEnumerator StopBinder_StopsTheSystem()
        {
            var (system, binder) = Create<ParticleSystemStopMonoBinder>();
            system.Play();
            yield return null;

            Invoke(binder);

            Assert.IsFalse(system.isPlaying, "Система не остановлена");
        }

        [UnityTest]
        public IEnumerator PauseBinder_PausesTheSystem()
        {
            var (system, binder) = Create<ParticleSystemPauseMonoBinder>();
            system.Play();
            yield return null;

            Invoke(binder);

            Assert.IsTrue(system.isPaused, "Система не поставлена на паузу");
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

            Assert.IsNotNull(received, "Команда не доехала до ViewModel");
            Assert.IsFalse(received.CanExecute(), "Команда согласилась выполниться на выключенном объекте");

            received.Execute();
            Assert.IsFalse(system.isPlaying, "Система запустилась на выключенном объекте");
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

            Assert.IsFalse(system.emission.enabled, "Эмиссия не выключена");
            Assert.IsTrue(system.isPlaying, "Система оказалась остановлена, а должна была продолжать играть");
        }

        [UnityTest]
        public IEnumerator TheSerializableTwin_AcceptsItsTarget()
        {
            NewParticleObject(out var system);
            yield return null;

            Assert.IsTrue(new ParticleSystemEmissionEnabledBinder(system).IsBind);
        }

        private static void Invoke(ParticleSystemPlaybackMonoBinder binder)
        {
            Action received = null;
            ((IBinder)binder).Bind(new OneWayToSourceBindableMember<Action>(value => received = value));

            Assert.IsNotNull(received, "Биндер не отдал действие во ViewModel");
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
            var gameObject = new GameObject("Particles");
            _spawned.Add(gameObject);

            system = gameObject.AddComponent<ParticleSystem>();
            system.Stop();

            return gameObject;
        }
    }
}
#endif
