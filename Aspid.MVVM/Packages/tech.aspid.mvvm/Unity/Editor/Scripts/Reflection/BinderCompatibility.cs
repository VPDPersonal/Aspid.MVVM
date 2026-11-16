#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides Inspector checks for the channels required by a binding mode.
    /// </summary>
    public static class BinderCompatibility
    {
        /// <summary>
        /// Determines whether the binder supplies every channel used by the member.
        /// </summary>
        /// <param name="binder">The binder to inspect.</param>
        /// <param name="valueType">The member's value type.</param>
        /// <param name="memberMode">The binding mode exposed by the member.</param>
        /// <param name="boxedChannels">When true, value-type members expose their Enum or ValueType channel.</param>
        /// <returns>True if every required channel is supported; otherwise, false.</returns>
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
