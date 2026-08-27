using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TwoWayValue{T}"/> pre-configured with <see cref="BindMode.OneWayToSource"/>,
    /// propagating the current value from the View back to the ViewModel on binding.
    /// </summary>
    /// <typeparam name="T">The type of the bindable value.</typeparam>
    /// <include file="XmlExampleDoc-Values-1.1.0.xml" path="doc//member[@name='OneWayToSourceValue{1}']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class OneWayToSourceValue<T> : TwoWayValue<T>
    {
        public OneWayToSourceValue()
            : base(BindMode.OneWayToSource) { }

        /// <param name="value">The initial value.</param>
        public OneWayToSourceValue(T? value)
            : base(value, BindMode.OneWayToSource) { }

        /// <remarks>
        /// The inherited converter is applied on the ViewModel → View path, in <see cref="IBinder{T}.SetValue"/>.
        /// <see cref="BindMode.OneWayToSource"/> has no such path, so a converter passed here is silently ignored
        /// and values reach the ViewModel unchanged. The overload is kept so existing code still compiles, but it
        /// warns rather than doing nothing quietly; convert in the ViewModel, or use <see cref="TwoWayValue{T}"/>
        /// in a mode that actually feeds the View.
        /// </remarks>
        /// <param name="value">The initial value.</param>
        /// <param name="converter">Ignored in this mode.</param>
        [Obsolete("A converter is only applied on the ViewModel -> View path, which BindMode.OneWayToSource does " +
                  "not have, so this one never runs. Convert in the ViewModel, or use TwoWayValue<T>.")]
        public OneWayToSourceValue(T? value, IConverter<T?, T?>? converter)
            : base(value, converter, BindMode.OneWayToSource) { }
    }
}