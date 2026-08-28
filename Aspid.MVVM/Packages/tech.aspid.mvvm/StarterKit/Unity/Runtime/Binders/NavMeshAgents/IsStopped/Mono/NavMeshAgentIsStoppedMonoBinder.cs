using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="NavMeshAgent.isStopped"/>.
    /// </summary>
    /// <remarks>The write is skipped while the agent is off the navmesh, since Unity throws for that case.</remarks>
    [AddBinderContextMenu(typeof(NavMeshAgent))]
    [AddComponentMenu("Aspid/MVVM/Binders/Navigation/NavMeshAgent Binder – Is Stopped")]
    public class NavMeshAgentIsStoppedMonoBinder : ComponentMonoBinder<NavMeshAgent, bool>
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
