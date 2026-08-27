using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinderWithConverter{T1, T2}">ComponentMonoBinderWithConverter&lt;TComponent, Quaternion&gt;</see> that binds a <see cref="Quaternion"/> property,
    /// implementing <see cref="IRotationBinder"/> and <see cref="INumberBinder"/>.
    /// <see cref="Vector2"/> and <see cref="Vector3"/> values are read as Euler angles, and a scalar
    /// (<see langword="int"/>, <see langword="long"/>, <see langword="float"/>, <see langword="double"/>)
    /// is applied as the same angle on all three axes.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target <see cref="Quaternion"/> property.</typeparam>
    public abstract partial class ComponentQuaternionMonoBinder<TComponent> : ComponentMonoBinderWithConverter<TComponent, Quaternion>,
        IRotationBinder,
        INumberBinder
        where TComponent : Component
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
