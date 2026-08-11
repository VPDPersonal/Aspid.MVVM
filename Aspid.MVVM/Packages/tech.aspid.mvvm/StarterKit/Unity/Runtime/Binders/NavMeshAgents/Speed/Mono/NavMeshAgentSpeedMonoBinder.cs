using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{NavMeshAgent}"/> that binds <see cref="NavMeshAgent.speed"/>.
    /// </summary>
    /// <remarks>
    /// How fast the agent moves — a slow, a haste, a state that walks instead of running. Clamped non-negative:
    /// a negative speed makes the agent refuse to move and reports nothing.
    /// </remarks>
    [AddBinderContextMenu(typeof(NavMeshAgent))]
    [AddComponentMenu("Aspid/MVVM/Binders/Navigation/NavMeshAgent Binder – Speed")]
    public class NavMeshAgentSpeedMonoBinder : ComponentFloatMonoBinder<NavMeshAgent>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.speed;
            set => CachedComponent.speed = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
