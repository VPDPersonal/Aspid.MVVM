using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that converts values of type <typeparamref name="TFrom"/>
    /// to <typeparamref name="TTo"/> using an <see cref="IConverter{TFrom,TTo}"/> and forwards the result to a target setter action.
    /// </summary>
    /// <typeparam name="TFrom">The source value type produced by the ViewModel binding.</typeparam>
    /// <typeparam name="TTo">The target value type expected by the setter action.</typeparam>
    /// <remarks>
    /// Only non-two-way bind modes are supported; passing <see cref="BindMode.TwoWay"/> will throw.
    /// </remarks>
    /// <include file="XmlExampleDoc-Generics-1.1.0.xml" path="doc//member[@name='GenericCasterBinder{2}']/*" />
    public class GenericCasterBinder<TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly Action<TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="GenericCasterBinder{TFrom,TTo}"/>.
        /// </summary>
        /// <param name="setValue">The action invoked with the converted <typeparamref name="TTo"/> value.</param>
        /// <param name="converter">The converter used to transform a <typeparamref name="TFrom"/> value to <typeparamref name="TTo"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        public GenericCasterBinder(
            Action<TTo?> setValue,
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
    /// together with a <typeparamref name="TTarget"/> instance, to a target-scoped setter action.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is being set.</typeparam>
    /// <typeparam name="TFrom">The source value type produced by the ViewModel binding.</typeparam>
    /// <typeparam name="TTo">The target value type expected by the setter action.</typeparam>
    /// <remarks>
    /// Passing the target separately enables method-group-style property setters on Unity components
    /// without capturing them in a closure.
    /// Only non-two-way bind modes are supported; passing <see cref="BindMode.TwoWay"/> will throw.
    /// </remarks>
    /// <include file="XmlExampleDoc-Generics-1.1.0.xml" path="doc//member[@name='GenericCasterBinder{3}']/*" />
    public class GenericCasterBinder<TTarget, TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly TTarget _target;
        private readonly Action<TTarget, TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="GenericCasterBinder{TTarget,TFrom,TTo}"/>.
        /// </summary>
        /// <param name="target">The target object whose property is updated on each value change.</param>
        /// <param name="setValue">
        /// The action invoked with the target and the converted <typeparamref name="TTo"/> value.
        /// </param>
        /// <param name="converter">The converter used to transform a <typeparamref name="TFrom"/> value to <typeparamref name="TTo"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/>, <paramref name="setValue"/>, or <paramref name="converter"/>
        /// is <see langword="null"/>.
        /// </exception>
        public GenericCasterBinder(
            TTarget target,
            Action<TTarget, TTo?> setValue,
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