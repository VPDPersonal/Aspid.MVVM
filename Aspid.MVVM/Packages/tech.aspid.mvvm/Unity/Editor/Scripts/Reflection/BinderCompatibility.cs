#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides utility methods for binder channel compatibility.
    /// </summary>
    public static class BinderCompatibility
    {
        /// <summary>
        /// Checks whether the binder supports every channel required by a member.
        /// </summary>
        /// <param name="binder">The binder to inspect.</param>
        /// <param name="valueType">The bound value type; <see langword="null"/> requires untyped binder channels.</param>
        /// <param name="memberMode">The binding mode exposed by the member.</param>
        /// <param name="boxedChannels">When <see langword="true"/>, also accepts <see cref="System.Enum"/> or <see cref="System.ValueType"/> channels for value types.</param>
        /// <returns><see langword="true"/> if all required channels are supported; otherwise, <see langword="false"/>.</returns>
        public static bool Matches(
            IBinder binder,
            Type? valueType,
            BindMode memberMode = BindMode.TwoWay,
            bool boxedChannels = true)
        {
            var mode = binder.Mode;
            if (mode is < BindMode.OneWay or > BindMode.OneWayToSource
                || memberMode is < BindMode.OneWay or > BindMode.OneWayToSource) return false;

            if (memberMode is BindMode.OneWay or BindMode.OneTime && mode is BindMode.OneWayToSource) return false;
            if (memberMode is BindMode.OneWayToSource && mode is BindMode.OneWay or BindMode.OneTime) return false;

            var forward = memberMode is not BindMode.OneWayToSource && mode is not BindMode.OneWayToSource;
            var reverse = memberMode is BindMode.TwoWay or BindMode.OneWayToSource
                && mode is BindMode.TwoWay or BindMode.OneWayToSource;
            var type = binder.GetType();
            return (!forward || Supports(typeof(IBinder<>), typeof(IAnyBinder)))
                && (!reverse || Supports(typeof(IReverseBinder<>), typeof(IAnyReverseBinder)));

            bool Supports(Type channel, Type any)
            {
                if (any.IsAssignableFrom(type)) return true;
                if (valueType is null || valueType.ContainsGenericParameters || valueType.IsByRef || valueType.IsPointer) return false;
                if (channel.MakeGenericType(valueType).IsAssignableFrom(type)) return true;
                if (!boxedChannels || !valueType.IsValueType) return false;

                var boxed = valueType.IsEnum ? typeof(Enum) : typeof(ValueType);
                return channel.MakeGenericType(boxed).IsAssignableFrom(type);
            }
        }
    }
}
