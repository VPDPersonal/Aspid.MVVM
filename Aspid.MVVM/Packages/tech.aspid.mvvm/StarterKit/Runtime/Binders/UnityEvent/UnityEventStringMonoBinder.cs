using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound value as a string.
    /// </summary>
    /// <remarks>
    /// Numbers are formatted with the configured culture; any other value uses <see cref="object.ToString"/>, and
    /// <see langword="null"/> becomes an empty string.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – String")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – String")]
    public sealed partial class UnityEventStringMonoBinder : MonoBinder,
        IAnyBinder,
        INumberBinder,
        IBinder<string>
    {
        [Tooltip("Culture numbers are formatted with.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [Tooltip("Optional converter applied to the text; empty leaves it as-is.")]
        [SerializeReference] private IConverter<string, string> _converter;

        [Tooltip("Invoked with the bound value.")]
        [SerializeField] private UnityEvent<string> _set;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(string value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value) =>
            SetValue(value?.ToString() ?? string.Empty);
    }
}
