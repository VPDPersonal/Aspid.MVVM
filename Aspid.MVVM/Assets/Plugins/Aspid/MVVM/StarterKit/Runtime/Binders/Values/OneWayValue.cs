using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that stores the most recently received
    /// ViewModel value of type <typeparamref name="T"/> and notifies subscribers when it changes.
    /// </summary>
    /// <typeparam name="T">The type of the bindable value.</typeparam>
    /// <remarks>
    /// Each time <see cref="IBinder{T}.SetValue"/> is called, the stored value is updated (passing
    /// through an optional <see cref="IConverter{TFrom,TTo}"/>) and the <see cref="Changed"/> event
    /// is raised with the original unconverted value.
    /// An implicit conversion operator allows instances to be used directly where a
    /// <typeparamref name="T"/> value is expected.
    /// Only non-two-way bind modes are supported; passing <see cref="BindMode.TwoWay"/> will throw.
    /// </remarks>
    /// <example>
    /// Subscribe to ViewModel value changes and update the View on each update.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private TMP_Text _label;
    ///     private OneWayValue&lt;int&gt; _score = new();
    ///     
    ///     private void OnInitializedInternal(IViewModel viewModel)
    ///     {
    ///         OnScoreChanged(_score.Value);
    ///         _score.Changed += OnScoreChanged;
    ///     }
    ///     
    ///     private void OnDeinitializingInternal()
    ///         _score.Changed -= OnScoreChanged;
    ///     
    ///     private void OnScoreChanged(int? value) =>
    ///         _label.text = value.ToString();
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public string _score;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class OneWayValue<T> : Binder, IBinder<T>
    {
        /// <summary>
        /// Raised with the new pre-conversion value when <see cref="Value"/> is updated.
        /// </summary>
        public event Action<T?>? Changed;

#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private T? _value;

#if UNITY_2023_1_OR_NEWER
        [UnityEngine.SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<T?, T?>? _converter;

        /// <summary>
        /// Gets the most recently received (and optionally converted) value.
        /// </summary>
        public T? Value
        {
            get => _value;
            private set => _value = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OneWayValue{T}"/> with the default value.
        /// </summary>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public OneWayValue(BindMode mode = BindMode.OneWay)
            : this(default, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="OneWayValue{T}"/> with a pre-set initial value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public OneWayValue(T? value, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OneWayValue{T}"/> with a pre-set initial value and a converter.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="converter">
        /// An optional converter applied to each incoming value before it is stored in <see cref="Value"/>.
        /// Pass <see langword="null"/> to store values unchanged.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public OneWayValue(T? value, IConverter<T?, T?>? converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();

            _value = value;
            _converter = converter;
        }

        /// <summary>
        /// Stores the incoming ViewModel value (passing it through the converter if one is set)
        /// and raises <see cref="Changed"/> with the original unconverted value.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        void IBinder<T>.SetValue(T? value)
        {
            Value = _converter is not null ? _converter.Convert(value) : value;
            Changed?.Invoke(value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="OneWayValue{T}"/> to its current <see cref="Value"/>.
        /// </summary>
        /// <param name="binder">The binder whose value is extracted.</param>
        /// <returns>The current value stored in <paramref name="binder"/>.</returns>
        public static implicit operator T?(OneWayValue<T?> binder) => binder.Value;
    }
}