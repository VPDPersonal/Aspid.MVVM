#nullable enable
using System;
using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{NavMeshAgent, bool}"/> that binds <see cref="NavMeshAgent.isStopped"/>.
    /// </summary>
    /// <remarks>The write is skipped while the agent is off the navmesh, since Unity throws for that case.</remarks>
    [Serializable]
    public class NavMeshAgentIsStoppedBinder : TargetBinder<NavMeshAgent, bool>
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
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
