using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder"/> that switches a target value between two pre-configured options
    /// based on a bound boolean ViewModel property.
    /// </summary>
    /// <typeparam name="T">The type of value to switch between.</typeparam>
    [Serializable]
    public abstract class SwitcherBinder<T> : Binder, IBinder<bool>
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Tooltip("Value applied when the bound boolean is true.")]
        [UnityEngine.SerializeField]
#endif
        private T _trueValue;

#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Tooltip("Value applied when the bound boolean is false.")]
        [UnityEngine.SerializeField]
#endif
        private T _falseValue;

        /// <summary>
        /// Initializes a new instance of SwitcherBinder/>.
        /// </summary>
        /// <param name="trueValue">The value forwarded when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The value forwarded when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        protected SwitcherBinder(T trueValue, T falseValue, BindMode mode)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();

            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Selects the appropriate value based on <paramref name="value"/> and forwards it to
        /// <see cref="SetValue(T)"/>.
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            SetValue(GetValue(value));

        /// <summary>
        /// Applies the selected <paramref name="value"/> to the underlying target.
        /// </summary>
        /// <param name="value">The value selected based on the boolean input.</param>
        protected abstract void SetValue(T value);

        private T GetValue(bool value) =>
            value ? _trueValue : _falseValue;
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget}"/> that switches a target property between two pre-configured options
    /// based on a bound boolean ViewModel property.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is switched.</typeparam>
    /// <typeparam name="T">The type of value to switch between.</typeparam>
    [Serializable]
    public abstract class SwitcherBinder<TTarget, T> : TargetBinder<TTarget>, IBinder<bool>
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Tooltip("Value applied when the bound boolean is true.")]
        [UnityEngine.SerializeField]
#endif
        private T _trueValue;

#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Tooltip("Value applied when the bound boolean is false.")]
        [UnityEngine.SerializeField]
#endif
        private T _falseValue;

        /// <summary>
        /// Initializes a new instance of SwitcherBinder/>.
        /// </summary>
        /// <param name="target">The target object that receives the resolved value.</param>
        /// <param name="trueValue">The value forwarded when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The value forwarded when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        protected SwitcherBinder(TTarget target, T trueValue, T falseValue, BindMode mode)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Selects the appropriate value based on <paramref name="value"/> and forwards it to
        /// <see cref="SetValue(T)"/>.
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            SetValue(value ? _trueValue : _falseValue);

        /// <summary>
        /// Applies the selected <paramref name="value"/> to the underlying target.
        /// </summary>
        /// <param name="value">The value selected based on the boolean input.</param>
        protected abstract void SetValue(T value);
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget}"/> that switches a target property between two pre-configured options
    /// based on a bound boolean ViewModel property, with optional value conversion via <typeparamref name="TConverter"/> before applying.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is switched.</typeparam>
    /// <typeparam name="T">The type of value to switch between.</typeparam>
    /// <typeparam name="TConverter">The converter type used to transform the selected value before applying it.</typeparam>
    [Serializable]
    public abstract class SwitcherBinder<TTarget, T, TConverter> : TargetBinder<TTarget>, IBinder<bool>
        where TConverter : class, IConverter<T?, T?>
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Tooltip("Value applied when the bound boolean is true.")]
        [UnityEngine.SerializeField]
#endif
        private T _trueValue;

#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Tooltip("Value applied when the bound boolean is false.")]
        [UnityEngine.SerializeField]
#endif
        private T _falseValue;

#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Tooltip("Optional converter applied to the selected value before it is set.")]
        [SerializeReferenceDropdown]
        [UnityEngine.SerializeReference]
#endif
        private TConverter? _converter;

        /// <summary>
        /// Initializes a new instance of SwitcherBinder with Converter/>.
        /// </summary>
        /// <param name="target">The target object that receives the resolved value.</param>
        /// <param name="trueValue">The value forwarded when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The value forwarded when the bound boolean is <see langword="false"/>.</param>
        /// <param name="converter">
        /// An optional converter applied to the selected value before it is forwarded to the target.
        /// Pass <see langword="null"/> to forward the value unchanged.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        protected SwitcherBinder(
            TTarget target,
            T trueValue,
            T falseValue,
            TConverter? converter,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _converter = converter;
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Selects the true or false value based on <paramref name="value"/>, converts it via <see cref="GetConvertedValue"/>,
        /// and forwards it to <see cref="SetValue(T?)"/>.
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            SetValue(GetConvertedValue(value ? _trueValue : _falseValue));

        /// <summary>
        /// Applies the selected and converted <paramref name="value"/> to the underlying target.
        /// </summary>
        /// <param name="value">The value selected and converted based on the boolean input.</param>
        protected abstract void SetValue(T? value);

        /// <summary>
        /// Converts <paramref name="value"/> using the serialized converter, or returns it unchanged if no converter is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual T? GetConvertedValue(T value) =>
            _converter is null ? value : _converter.Convert(value);
    }
}