using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinderWithConverter{TProperty}">MonoBinderWithConverter&lt;Vector3&gt;</see> that binds a <see cref="Vector3"/> property,
    /// implementing <see cref="IVectorBinder"/> and <see cref="INumberBinder"/>.
    /// Scalar values (<see langword="int"/>, <see langword="long"/>, <see langword="float"/>, <see langword="double"/>)
    /// are broadcast to all three vector components as <c>new Vector3(value, value, value)</c>.
    /// </summary>
    public abstract partial class Vector3MonoBinder : MonoBinderWithConverter<Vector3>,
        IVectorBinder,
        INumberBinder
    {
        /// <summary>
        /// Sets the bound property to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The vector to apply.</param>
        /// <remarks>
        /// Redeclared rather than inherited on purpose. Overload resolution stops at the most derived type that
        /// declares an applicable <c>SetValue</c>, and <see cref="Vector3"/> converts implicitly to
        /// <see cref="Vector2"/> — without this member a direct <c>SetValue(someVector3)</c> would bind to
        /// <see cref="SetValue(Vector2)"/> and silently drop Z.
        /// </remarks>
        [BinderLog]
        public new void SetValue(Vector3 value) =>
            base.SetValue(value);

        /// <summary>
        /// Sets the bound property by promoting <paramref name="value"/> to a <see cref="Vector3"/> with Z set to zero.
        /// </summary>
        /// <param name="value">The 2D vector to promote.</param>
        [BinderLog]
        public void SetValue(Vector2 value) =>
            base.SetValue(value);

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector3"/>(<paramref name="value"/>, <paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to all three vector components.</param>
        [BinderLog]
        public void SetValue(int value) =>
            base.SetValue(new Vector3(value, value, value));

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector3"/>(<paramref name="value"/>, <paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to all three vector components.</param>
        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue(new Vector3(value, value, value));

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector3"/>(<paramref name="value"/>, <paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to all three vector components.</param>
        [BinderLog]
        public void SetValue(float value) =>
            base.SetValue(new Vector3(value, value, value));

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector3"/>(<paramref name="value"/>, <paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to all three vector components. Narrowed to <see langword="float"/> — precision may be lost.</param>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}
