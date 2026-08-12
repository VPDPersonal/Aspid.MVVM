#nullable enable
using System;
using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{NavMeshAgent}"/> that binds <see cref="NavMeshAgent.speed"/>.
    /// </summary>
    /// <remarks>
    /// How fast the agent moves — a slow, a haste, a state that walks instead of running. Clamped non-negative:
    /// a negative speed makes the agent refuse to move and reports nothing.
    /// </remarks>
    [Serializable]
    public class NavMeshAgentSpeedBinder : TargetFloatBinder<NavMeshAgent>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.speed;
            set => Target.speed = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }

        /// <inheritdoc/>
        public NavMeshAgentSpeedBinder(
            NavMeshAgent target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
