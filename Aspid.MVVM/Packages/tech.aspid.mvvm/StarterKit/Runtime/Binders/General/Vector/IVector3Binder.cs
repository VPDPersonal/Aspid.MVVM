using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IVectorBinder"/> whose implementors bind a <see cref="Vector3"/>: a <see cref="Vector2"/> is
    /// promoted with Z set to zero, and a scalar is applied to all three components.
    /// </summary>
    public interface IVector3Binder : IVectorBinder
    {
        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<Vector2>.SetValue(Vector2 value) =>
            SetValue(new Vector3(value.x, value.y, 0f));
    }
}
