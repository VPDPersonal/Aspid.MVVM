using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IVectorBinder"/> whose implementors bind a <see cref="Vector2"/>: a <see cref="Vector3"/> is
    /// accepted by dropping its Z component, and a scalar is applied to both components.
    /// </summary>
    public interface IVector2Binder : IVectorBinder
    {
        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<float>.SetValue(float value) =>
            SetValue(new Vector2(value, value));

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<Vector3>.SetValue(Vector3 value) =>
            SetValue(new Vector2(value.x, value.y));
    }
}
