using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{NavMeshAgent}"/> that binds <see cref="NavMeshAgent.isStopped"/>.
    /// </summary>
    /// <remarks>
    /// Whether the agent holds position — a stun, a conversation, a cutscene. The write is skipped while the agent
    /// is not on a navmesh: Unity throws for that case, and an exception inside a binding loop would take the rest
    /// of the View's bindings with it.
    /// </remarks>
    [AddBinderContextMenu(typeof(NavMeshAgent))]
    [AddComponentMenu("Aspid/MVVM/Binders/Navigation/NavMeshAgent Binder – Is Stopped")]
    public class NavMeshAgentIsStoppedMonoBinder : ComponentBoolMonoBinder<NavMeshAgent>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.isStopped;
            set
            {
                // isStopped бросается, если агент не на навмеше: у только что созданного или выключенного
                // агента запись превращается в исключение прямо в цикле привязки.
                if (!CachedComponent.isOnNavMesh) return;
                CachedComponent.isStopped = value;
            }
        }
    }
}
