using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinderWithConverter{TProperty}">MonoBinderWithConverter&lt;Vector2&gt;</see> that binds a <see cref="Vector2"/> property,
    /// implementing <see cref="IVectorBinder"/> and <see cref="INumberBinder"/>.
    /// A <see cref="Vector3"/> is accepted by dropping its Z component, and scalar values
    /// (<see langword="int"/>, <see langword="long"/>, <see langword="float"/>, <see langword="double"/>)
    /// are broadcast to both vector components as <c>new Vector2(value, value)</c>.
    /// </summary>
    public abstract partial class Vector2MonoBinder : MonoBinderWithConverter<Vector2>,
        IVectorBinder,
        INumberBinder
    {
        /// <summary>
        /// Sets the bound property to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The vector to apply.</param>
        /// <remarks>
        /// Redeclared rather than inherited on purpose. Overload resolution stops at the most derived type that
        /// declares an applicable <c>SetValue</c>, and <see cref="Vector2"/> converts implicitly to
        /// <see cref="Vector3"/> — without this member a direct <c>SetValue(someVector2)</c> would bind to
        /// <see cref="SetValue(Vector3)"/> and make a round trip through a three-component vector.
        /// </remarks>
        [BinderLog]
        public new void SetValue(Vector2 value) =>
            base.SetValue(value);

        /// <summary>
        /// Sets the bound property by dropping the Z component of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The 3D vector whose X and Y components are applied.</param>
        [BinderLog]
        public void SetValue(Vector3 value) =>
            base.SetValue(value);

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector2"/>(<paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to both vector components.</param>
        [BinderLog]
        public void SetValue(int value) =>
            base.SetValue(new Vector2(value, value));

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector2"/>(<paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to both vector components.</param>
        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue(new Vector2(value, value));

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector2"/>(<paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to both vector components.</param>
        [BinderLog]
        public void SetValue(float value) =>
            base.SetValue(new Vector2(value, value));

        /// <summary>
        /// Sets the bound property to <c>new <see cref="Vector2"/>(<paramref name="value"/>, <paramref name="value"/>)</c>.
        /// </summary>
        /// <param name="value">The scalar value applied to both vector components. Narrowed to <see langword="float"/> — precision may be lost.</param>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}
