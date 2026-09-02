using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound numeric ViewModel value converted to <see cref="double"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(double))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Double")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Double")]
    public sealed partial class UnityEventDoubleMonoBinder : MonoBinder, IDoubleBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<double, double> _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<double> _set;
        
        /// <summary>
        /// Invokes the event with the specified double value, applying the converter if configured.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(double value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
