#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="DynamicProperty{T}"/> with typed and boxed struct binding channels.
    /// </summary>
    /// <typeparam name="T">The property's struct type.</typeparam>
    public class DynamicStructProperty<T> : DynamicProperty<T>
        where T : struct
    {
        /// <summary>
        /// Creates a property with typed and boxed binding channels.
        /// </summary>
        /// <remarks>
        /// OneWayToSource exposes a TwoWay member, matching <see cref="DynamicProperty{T}"/>.
        /// </remarks>
        /// <param name="id">The identifier used by binders to resolve the property.</param>
        /// <param name="value">The initial value.</param>
        /// <param name="mode">The binding mode. Must not be None or undefined.</param>
        /// <exception cref="ArgumentException"><paramref name="id"/> is blank or <paramref name="mode"/> is None.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> is undefined.</exception>
        public DynamicStructProperty(
            string id,
            T value = default,
            BindMode mode = BindMode.OneWay)
            : base(id, value, mode, CreateMember) { }

        private static (IBinderAdder Member, Action<T> Publish) CreateMember(
            BindMode mode,
            T value,
            Action<T> setter)
        {
            switch (mode)
            {
                case BindMode.OneTime:
                    var oneTime = new OneTimeStructBindableMember<T>(value);
                    return (oneTime, next => oneTime.Value = next);

                case BindMode.OneWay:
                    var oneWay = new OneWayStructBindableMember<T>(value);
                    return (oneWay, next => oneWay.Value = next);

                case BindMode.TwoWay:
                case BindMode.OneWayToSource:
                    var twoWay = new TwoWayStructBindableMember<T>(value, setter);
                    return (twoWay, next => twoWay.Value = next);

                default: throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported binding mode.");
            }
        }
    }
}
