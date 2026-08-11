using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the two external systems the audit left for last: <see cref="VideoPlayer"/> and
    /// <see cref="NavMeshAgent"/>.
    /// </summary>
    /// <remarks>
    /// Both are modules a project may or may not have, and both were listed as reasonable only once the rest was covered.
    /// The properties bound here are the ones a ViewModel actually decides — which video plays and how fast, how fast an
    /// agent moves and whether it moves at all.
    /// </remarks>
    [TestFixture]
    public sealed class ExternalSystemTests
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
        public void PlaybackSpeed_IsClampedToTheDocumentedRange()
        {
            var player = New<VideoPlayer>();
            var binder = player.gameObject.AddComponent<VideoPlayerPlaybackSpeedMonoBinder>();

            ((IBinder<float>)binder).SetValue(2f);
            Assert.AreEqual(2f, player.playbackSpeed, 0.001f, "Скорость воспроизведения не доехала");

            ((IBinder<float>)binder).SetValue(100f);
            Assert.AreEqual(10f, player.playbackSpeed, 0.001f, "Скорость вне 0..10 не обрезана");

            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(player.playbackSpeed), "NaN дошёл до плеера");
        }

        [Test]
        public void IsLooping_ReachesThePlayer()
        {
            var player = New<VideoPlayer>();
            var binder = player.gameObject.AddComponent<VideoPlayerIsLoopingMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);

            Assert.IsTrue(player.isLooping, "Зацикливание не доехало");
        }

        [Test]
        public void AgentSpeed_ReachesTheAgent_AndIsNeverNegative()
        {
            var agent = New<NavMeshAgent>();
            var binder = agent.gameObject.AddComponent<NavMeshAgentSpeedMonoBinder>();

            ((IBinder<float>)binder).SetValue(4f);
            Assert.AreEqual(4f, agent.speed, 0.001f, "Скорость агента не доехала");

            ((IBinder<float>)binder).SetValue(-2f);
            Assert.AreEqual(0f, agent.speed, 0.001f, "Отрицательная скорость не обрезана");
        }

        /// <summary>
        /// Writing <see cref="NavMeshAgent.isStopped"/> off a navmesh throws, and an exception inside a binding loop takes
        /// the rest of the View's bindings with it — a test agent is never on one.
        /// </summary>
        [Test]
        public void IsStopped_IsSkippedWhileTheAgentIsNotOnANavMesh()
        {
            var agent = New<NavMeshAgent>();
            var binder = agent.gameObject.AddComponent<NavMeshAgentIsStoppedMonoBinder>();

            Assert.IsFalse(agent.isOnNavMesh, "Тестовый агент неожиданно оказался на навмеше");
            Assert.DoesNotThrow(() => ((IBinder<bool>)binder).SetValue(true), "Запись вне навмеша бросила исключение");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var player = New<VideoPlayer>();
            var agent = New<NavMeshAgent>();

            Assert.IsTrue(new VideoPlayerPlaybackSpeedBinder(player).IsBind);
            Assert.IsTrue(new VideoPlayerIsLoopingBinder(player).IsBind);
            Assert.IsTrue(new VideoPlayerClipBinder(player).IsBind);
            Assert.IsTrue(new NavMeshAgentSpeedBinder(agent).IsBind);
            Assert.IsTrue(new NavMeshAgentIsStoppedBinder(agent).IsBind);
        }

        private T New<T>()
            where T : Component
        {
            var gameObject = new GameObject(typeof(T).Name);
            _spawned.Add(gameObject);

            return gameObject.AddComponent<T>();
        }
    }
}
