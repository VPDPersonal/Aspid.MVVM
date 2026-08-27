using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinderWithConverter{TProperty}">MonoBinderWithConverter&lt;Quaternion&gt;</see> that binds a <see cref="Quaternion"/> property,
    /// implementing <see cref="IRotationBinder"/> and <see cref="INumberBinder"/>.
    /// <see cref="Vector2"/> and <see cref="Vector3"/> values are read as Euler angles, and a scalar
    /// (<see langword="int"/>, <see langword="long"/>, <see langword="float"/>, <see langword="double"/>)
    /// is applied as the same angle on all three axes.
    /// </summary>
    public abstract partial class QuaternionMonoBinder : MonoBinderWithConverter<Quaternion>,
        IRotationBinder,
        INumberBinder
    {
        /// <summary>
        /// Converts the value to a <see langword="float"/> and applies a uniform <see cref="Quaternion.Euler(float, float, float)"/> rotation.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see langword="float"/> and applies a uniform <see cref="Quaternion.Euler(float, float, float)"/> rotation.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see langword="float"/> and applies a uniform <see cref="Quaternion.Euler(float, float, float)"/> rotation.
        /// </summary>
        /// <param name="value">The value received from the ViewModel. Narrowed to <see langword="float"/> — precision may be lost.</param>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);

        /// <summary>
        /// Applies a uniform <see cref="Quaternion.Euler(float, float, float)"/> rotation using the given angle on all three axes.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            base.SetValue(Quaternion.Euler(new Vector3(value, value, value)));
    }
}
