#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinderWithConverter{T1, T2}">TargetBinderWithConverter&lt;TTarget, Quaternion&gt;</see> that binds a <see cref="Quaternion"/> property,
    /// implementing <see cref="IRotationBinder"/> and <see cref="INumberBinder"/>.
    /// <see cref="Vector2"/> and <see cref="Vector3"/> values are read as Euler angles, and a scalar
    /// (<see langword="int"/>, <see langword="long"/>, <see langword="float"/>, <see langword="double"/>)
    /// is applied as the same angle on all three axes.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see cref="Quaternion"/> property.</typeparam>
    public abstract class TargetQuaternionBinder<TTarget> : TargetBinderWithConverter<TTarget, Quaternion>,
        IRotationBinder,
        INumberBinder
    {
        /// <param name="target">The target object that owns the rotation property.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Quaternion"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — a rotation property raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        protected TargetQuaternionBinder(TTarget target, IConverter<Quaternion, Quaternion>? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Converts the value to a <see langword="float"/> and applies a uniform <see cref="Quaternion.Euler(float, float, float)"/> rotation.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see langword="float"/> and applies a uniform <see cref="Quaternion.Euler(float, float, float)"/> rotation.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see langword="float"/> and applies a uniform <see cref="Quaternion.Euler(float, float, float)"/> rotation.
        /// </summary>
        /// <param name="value">The value received from the ViewModel. Narrowed to <see langword="float"/> — precision may be lost.</param>
        public void SetValue(double value) =>
            SetValue((float)value);

        /// <summary>
        /// Applies a uniform <see cref="Quaternion.Euler(float, float, float)"/> rotation using the given angle on all three axes.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(float value) =>
            base.SetValue(Quaternion.Euler(new Vector3(value, value, value)));

    }
}
