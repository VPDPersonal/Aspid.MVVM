using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{NavMeshAgent}"/> that binds <see cref="NavMeshAgent.isStopped"/>.
    /// </summary>
    /// <remarks>The write is skipped while the agent is off the navmesh, since Unity throws for that case.</remarks>
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
                // isStopped throws when the agent isn't on a navmesh.
                if (!CachedComponent.isOnNavMesh) return;
                CachedComponent.isStopped = value;
            }
        }
    }
}
