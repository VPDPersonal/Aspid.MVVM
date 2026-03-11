#nullable enable
using System;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that converts values of type <typeparamref name="TFrom"/>
    /// to <typeparamref name="TTo"/> using an <see cref="IConverter{TFrom, TTo}"/> and forwards the result
    /// to a <see cref="UnityAction{T}"/> setter.
    /// </summary>
    /// <typeparam name="TFrom">The source value type produced by the ViewModel binding.</typeparam>
    /// <typeparam name="TTo">The target value type expected by the setter action.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericCasterBinder{TFrom,TTo}"/> that accepts a
    /// <see cref="UnityAction{T}"/> instead of a plain <see cref="System.Action{T}"/>.
    /// Only non-two-way bind modes are supported; passing <see cref="BindMode.TwoWay"/> will throw.
    /// </remarks>
    /// <example>
    /// Convert a float ViewModel value to a formatted string for a label
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private TMP_Text _label;
    ///     
    ///     private UnityGenericCasterBinder&lt;float, string&gt; Distance = new
    ///     (
    ///         value => _label.text = value,
    ///         new FloatToStringConverter("F1")
    ///     );
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _distance;
    /// }
    /// </code>
    /// </example>
    public class UnityGenericCasterBinder<TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly UnityAction<TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericCasterBinder{TFrom,TTo}"/>.
        /// </summary>
        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked with the converted <typeparamref name="TTo"/> value.</param>
        /// <param name="converter">The converter used to transform a <typeparamref name="TFrom"/> value to <typeparamref name="TTo"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        public UnityGenericCasterBinder(
            UnityAction<TTo?> setValue, 
            IConverter<TFrom?, TTo?> converter,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to <typeparamref name="TTo"/> and forwards the result to the target setter.
        /// </summary>
        /// <param name="value">The source value to convert and forward.</param>
        public void SetValue(TFrom? value) =>
            _setValue(_converter.Convert(value));
    }
    
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that converts values of type <typeparamref name="TFrom"/>
    /// to <typeparamref name="TTo"/> using an <see cref="IConverter{TFrom,TTo}"/> and forwards the result,
    /// together with a <typeparamref name="TTarget"/> instance, to a target-scoped <see cref="UnityAction{T0,T1}"/> setter.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is being set.</typeparam>
    /// <typeparam name="TFrom">The source value type produced by the ViewModel binding.</typeparam>
    /// <typeparam name="TTo">The target value type expected by the setter action.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericCasterBinder{TTarget,TFrom,TTo}"/> that accepts a
    /// <see cref="UnityAction{T0,T1}"/> instead of a plain <see cref="System.Action{T1,T2}"/>.
    /// Passing the target separately enables method-group-style property setters on Unity components
    /// without capturing them in a closure.
    /// Only non-two-way bind modes are supported; passing <see cref="BindMode.TwoWay"/> will throw.
    /// </remarks>
    /// <example>
    /// Target-scoped variant avoids capturing the label in a closure
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private TMP_Text _label;
    ///     
    ///     private UnityGenericCasterBinder&lt;TMP_Text, float, string&gt; Distance = new
    ///     (
    ///         _label,
    ///         (label, value) => label.text = value,
    ///         new FloatToStringConverter("F1")
    ///     );
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _distance;
    /// }
    /// </code>
    /// </example>
    public class UnityGenericCasterBinder<TTarget, TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericCasterBinder{TTarget,TFrom,TTo}"/>.
        /// </summary>
        /// <param name="target">The target object whose property is updated on each value change.</param>
        /// <param name="setValue">
        /// The <see cref="UnityAction{T0,T1}"/> invoked with the target and the converted <typeparamref name="TTo"/> value.
        /// </param>
        /// <param name="converter">The converter used to transform a <typeparamref name="TFrom"/> value to <typeparamref name="TTo"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/>, <paramref name="setValue"/>, or <paramref name="converter"/>
        /// is <see langword="null"/>.
        /// </exception>
        public UnityGenericCasterBinder(
            TTarget target,
            UnityAction<TTarget, TTo?> setValue,
            IConverter<TFrom?, TTo?> converter,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to <typeparamref name="TTo"/> and forwards the result to the target setter.
        /// </summary>
        /// <param name="value">The source value to convert and forward.</param>
        public void SetValue(TFrom? value) =>
            _setValue(_target, _converter.Convert(value));
    }
}