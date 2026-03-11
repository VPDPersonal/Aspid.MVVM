using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that converts a <see cref="string"/> value to a <see cref="bool"/>
    /// using a configurable converter before forwarding it to a target setter.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="StringEmptyToBoolConverter"/> which treats a non-empty, non-null string as
    /// <see langword="true"/>. The result can be inverted via the <c>isInvert</c> flag, or replaced entirely with
    /// a custom <see cref="IConverter{TFrom,TTo}"/>.
    /// </remarks>
    /// <example>
    /// Show a warning panel when a ViewModel error message is non-empty.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private GameObject _warningPanel;
    ///
    ///     private StringToBoolCasterBinder ErrorMessage => new
    ///     (
    ///         value => _warningPanel.SetActive(value),
    ///         isInvert: true
    ///     );
    /// }
    ///
    ///
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public string _errorMessage;
    /// }
    /// </code>
    /// </example>
    public sealed class StringToBoolCasterBinder : Binder, IBinder<string>
    {
        private readonly Action<bool> _setValue;
        private readonly IConverter<string?, bool> _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="StringToBoolCasterBinder"/> using the default
        /// non-inverted <see cref="StringEmptyToBoolConverter"/>.
        /// </summary>
        /// <param name="setValue">The action invoked with the converted <see cref="bool"/> value.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public StringToBoolCasterBinder(Action<bool> setValue, BindMode mode)
            : this(setValue, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="StringToBoolCasterBinder"/> with an optional inversion flag.
        /// </summary>
        /// <param name="setValue">The action invoked with the converted <see cref="bool"/> value.</param>
        /// <param name="isInvert">
        /// When <see langword="true"/>, the conversion result is logically negated so that an empty or
        /// <see langword="null"/> string maps to <see langword="true"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public StringToBoolCasterBinder(Action<bool> setValue, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : this(setValue, new StringEmptyToBoolConverter(isInvert), mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="StringToBoolCasterBinder"/> with a custom converter.
        /// </summary>
        /// <param name="setValue">The action invoked with the converted <see cref="bool"/> value.</param>
        /// <param name="converter">The converter used to transform a <see cref="string"/> to a <see cref="bool"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        public StringToBoolCasterBinder(Action<bool> setValue, IConverter<string?, bool> converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="bool"/> and forwards the result to the target setter.
        /// </summary>
        /// <param name="value">The source string value to convert and forward.</param>
        public void SetValue(string? value) =>
            _setValue(_converter.Convert(value));
    }
}