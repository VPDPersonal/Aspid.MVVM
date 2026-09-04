using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="NavMeshAgent.speed"/>.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(NavMeshAgent), serializePropertyNames: "m_Speed")]
    [AddComponentMenu("Aspid/MVVM/Binders/Navigation/NavMeshAgent Binder – Speed")]
    public class NavMeshAgentSpeedMonoBinder : ComponentFloatMonoBinder<NavMeshAgent>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.speed;
            set => CachedComponent.speed = this.NonNegative(value);
        }
    }
}
