using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IFloatBinder"/> that also accepts <see cref="Vector2"/> and <see cref="Vector3"/> values,
    /// applying a scalar to every component.
    /// </summary>
    public interface IVectorBinder : IBinder<Vector2>, IBinder<Vector3>, IFloatBinder
    {
        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<float>.SetValue(float value) =>
            SetValue(new Vector3(value, value, value));
    }
}
