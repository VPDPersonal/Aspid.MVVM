using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Bool")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Bool")]
    public sealed partial class UnityEventBoolMonoBinder : MonoBinder, IBinder<bool>
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;
        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<bool> _set;

        /// <summary>
        /// Invokes the event with the specified boolean value, applying the configured converter if present.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
