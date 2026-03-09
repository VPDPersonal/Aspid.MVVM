#nullable enable
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget,TProperty,TConverter}"/> that binds a <see cref="Vector3"/> property,
    /// implementing <see cref="IVectorBinder"/> and <see cref="INumberBinder"/>.
    /// Scalar values (<see langword="int"/>, <see langword="long"/>, <see langword="float"/>, <see langword="double"/>)
    /// are broadcast to all three vector components as <c>new Vector3(value, value, value)</c>.
    /// </summary>
    /// <typeparam name="TTarget">The type of <see cref="Component"/> that exposes the target <see cref="Vector3"/> property.</typeparam>
    public abstract class TargetVector3Binder<TTarget> : TargetBinder<TTarget, Vector3, Converter>,
        IVectorBinder,
        INumberBinder
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TargetVector3Binder{TTarget}"/>.
        /// </summary>
        /// <param name="target">The target object whose Vector3 property is managed by this binder.</param>
        /// <param name="converter">An optional Vector3-to-Vector3 converter applied before the value is stored.</param>
        /// <param name="mode">The binding mode to use.</param>
        protected TargetVector3Binder(TTarget target, Converter? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <summary>
        /// Sets the bound property by promoting <paramref name="value"/> to a <see cref="Vector3"/> with Z set to zero.
        /// </summary>
        /// <param name="value">The 2D vector to promote.</param>
        public void SetValue(Vector2 value) =>
            base.SetValue(value);

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector3"/>(<paramref name="value"/>, <paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to all three vector components.</param>
        public void SetValue(int value) =>
            base.SetValue(new Vector3(value, value, value));

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector3"/>(<paramref name="value"/>, <paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to all three vector components.</param>
        public void SetValue(long value) =>
            base.SetValue(new Vector3(value, value, value));

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector3"/>(<paramref name="value"/>, <paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to all three vector components.</param>
        public void SetValue(float value) =>
            base.SetValue(new Vector3(value, value, value));

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector3"/>(<paramref name="value"/>, <paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to all three vector components. Narrowed to <see langword="float"/> — precision may be lost.</param>
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}