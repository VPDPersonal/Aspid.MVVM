using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see langword="double"/>.
    /// </summary>
    /// <remarks>
    /// Also accepts the other numeric types.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(double))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Double")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Double")]
    public sealed partial class UnityEventDoubleMonoBinder : MonoBinder, IDoubleBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<double, double> _converter;

        [Tooltip("Invoked with the bound value.")]
        [SerializeField] private UnityEvent<double> _set;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(double value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
