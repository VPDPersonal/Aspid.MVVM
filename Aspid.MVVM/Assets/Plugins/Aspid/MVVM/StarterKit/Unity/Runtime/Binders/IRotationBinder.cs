using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extends <see cref="IVectorBinder"/> with <see cref="Quaternion"/> binding support,
    /// converting <see cref="Vector2"/> and <see cref="Vector3"/> Euler angles to a <see cref="Quaternion"/> before applying.
    /// </summary>
    public interface IRotationBinder : IVectorBinder, IBinder<Quaternion>
    {
        /// <summary>
        /// Converts <paramref name="value"/> from Euler angles to a <see cref="Quaternion"/> and applies it.
        /// </summary>
        /// <param name="value">Euler angles (X, Y) to convert and apply.</param>
        void IBinder<Vector2>.SetValue(Vector2 value) =>
            SetValue(Quaternion.Euler(value));

        /// <summary>
        /// Converts <paramref name="value"/> from Euler angles to a <see cref="Quaternion"/> and applies it.
        /// </summary>
        /// <param name="value">Euler angles (X, Y, Z) to convert and apply.</param>
        void IBinder<Vector3>.SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
    }
}