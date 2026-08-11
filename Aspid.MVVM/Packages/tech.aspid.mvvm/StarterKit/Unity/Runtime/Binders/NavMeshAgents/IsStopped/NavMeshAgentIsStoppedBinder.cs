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
    /// <remarks>
    /// Whether the agent holds position — a stun, a conversation, a cutscene. The write is skipped while the agent
    /// is not on a navmesh: Unity throws for that case, and an exception inside a binding loop would take the rest
    /// of the View's bindings with it.
    /// </remarks>
    [Serializable]
    public class NavMeshAgentIsStoppedBinder : TargetBoolBinder<NavMeshAgent>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.isStopped;
            set
            {
                // isStopped бросается, если агент не на навмеше: у только что созданного или выключенного
                // агента запись превращается в исключение прямо в цикле привязки.
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
