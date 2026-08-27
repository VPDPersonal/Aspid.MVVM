using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see cref="Color"/> ViewModel value.
    /// </summary>
    [AddBinderContextMenuByType(typeof(Color))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Color")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Color")]
    public sealed partial class UnityEventColorMonoBinder : MonoBinder, IColorBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<Color, Color> _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<Color> _set;

        /// <summary>
        /// Invokes the event with the specified <see cref="Color"/> value, applying the converter if configured.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Color value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
