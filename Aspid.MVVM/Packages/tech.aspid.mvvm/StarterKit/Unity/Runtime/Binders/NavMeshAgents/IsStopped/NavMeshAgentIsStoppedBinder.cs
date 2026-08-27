#nullable enable
using System;
using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{NavMeshAgent}"/> that binds <see cref="NavMeshAgent.isStopped"/>.
    /// </summary>
    /// <remarks>The write is skipped while the agent is off the navmesh, since Unity throws for that case.</remarks>
    [Serializable]
    public class NavMeshAgentIsStoppedBinder : TargetBoolBinder<NavMeshAgent>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.isStopped;
            set
            {
                // isStopped throws when the agent isn't on a navmesh.
                if (!Target.isOnNavMesh) return;
                Target.isStopped = value;
            }
        }

        /// <inheritdoc/>
        public NavMeshAgentIsStoppedBinder(
            NavMeshAgent target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
