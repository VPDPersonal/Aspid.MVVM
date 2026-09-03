using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IVectorBinder"/> whose implementors bind a <see cref="Quaternion"/>: vectors are read as Euler angles,
    /// a scalar as the same angle on all three axes.
    /// </summary>
    public interface IRotationBinder : IVectorBinder, IBinder<Quaternion>
    {
        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<Vector2>.SetValue(Vector2 value) =>
            SetValue(Quaternion.Euler(value));

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<Vector3>.SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
    }
}
