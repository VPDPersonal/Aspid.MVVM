using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see cref="Quaternion"/> ViewModel value.
    /// </summary>
    [AddBinderContextMenuByType(typeof(Quaternion))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Quaternion")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Quaternion")]
    public sealed partial class UnityEventQuaternionMonoBinder : MonoBinder, IRotationBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<Quaternion, Quaternion> _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<Quaternion> _set;

        /// <summary>
        /// Invokes the event with the specified <see cref="Quaternion"/> value, applying the converter if configured.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Quaternion value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}
