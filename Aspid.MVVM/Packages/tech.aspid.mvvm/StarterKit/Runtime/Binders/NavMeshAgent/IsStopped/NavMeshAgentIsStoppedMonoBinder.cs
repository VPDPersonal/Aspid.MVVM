using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="NavMeshAgent.isStopped"/>.
    /// </summary>
    /// <remarks>
    /// Unity throws for an agent that is not on a NavMesh; such a write is reported and skipped, and the read
    /// returns <see langword="false"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(NavMeshAgent))]
    [AddComponentMenu("Aspid/MVVM/Binders/Navigation/NavMeshAgent Binder – Is Stopped")]
    public class NavMeshAgentIsStoppedMonoBinder : ComponentMonoBinder<NavMeshAgent, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.isOnNavMesh && CachedComponent.isStopped;
            set
            {
                if (CachedComponent.isOnNavMesh)
                {
                    CachedComponent.isStopped = value;
                    return;
                }

                this.LogError(
                    problem: "the agent is not on a NavMesh",
                    consequence: "isStopped is left unchanged.");
            }
        }
    }
}
