using UnityEngine;
using UnityEngine.AI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="NavMeshAgent"/> binders: speed and the stopped flag.
    /// </summary>
    [TestFixture]
    public sealed class NavMeshAgentBinderTests : SceneFixture
    {
        [Test]
        public void Speed_ReachesTheAgent_AndIsNeverNegative()
        {
            var agent = Spawn<NavMeshAgent>("NavMeshAgent");
            var binder = agent.gameObject.AddComponent<NavMeshAgentSpeedMonoBinder>();

            ((IBinder<float>)binder).SetValue(4f);
            Assert.AreEqual(4f, agent.speed, 0.001f, "The speed did not reach the agent");

            ((IBinder<float>)binder).SetValue(-2f);
            Assert.AreEqual(0f, agent.speed, 0.001f, "A negative speed was not clamped");
        }

        /// <summary>
        /// Writing <see cref="NavMeshAgent.isStopped"/> off a navmesh throws, and an exception inside a binding loop takes
        /// the rest of the View's bindings with it — a test agent is never on one.
        /// </summary>
        [Test]
        public void IsStopped_IsSkippedWhileTheAgentIsNotOnANavMesh()
        {
            var agent = Spawn<NavMeshAgent>("NavMeshAgent");
            var binder = agent.gameObject.AddComponent<NavMeshAgentIsStoppedMonoBinder>();

            Assert.IsFalse(agent.isOnNavMesh, "The test agent unexpectedly ended up on a navmesh");
            Assert.DoesNotThrow(() => ((IBinder<bool>)binder).SetValue(true), "Writing off the navmesh threw");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var agent = Spawn<NavMeshAgent>("NavMeshAgent");

            Assert.IsTrue(new NavMeshAgentSpeedBinder(agent).CanBind);
            Assert.IsTrue(new NavMeshAgentIsStoppedBinder(agent).CanBind);
        }
    }
}
