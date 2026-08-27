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
    /// <remarks>Clamped to non-negative — a negative speed would leave the agent unable to move.</remarks>
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
