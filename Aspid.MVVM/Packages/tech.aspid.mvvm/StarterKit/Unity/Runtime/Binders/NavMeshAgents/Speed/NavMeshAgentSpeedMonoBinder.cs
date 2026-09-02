using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{NavMeshAgent}"/> that binds <see cref="NavMeshAgent.speed"/>.
    /// </summary>
    /// <remarks>Clamped to non-negative — a negative speed would leave the agent unable to move.</remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(NavMeshAgent))]
    [AddComponentMenu("Aspid/MVVM/Binders/Navigation/NavMeshAgent Binder – Speed")]
    public class NavMeshAgentSpeedMonoBinder : ComponentFloatMonoBinder<NavMeshAgent>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.speed;
            set => CachedComponent.speed = this.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
