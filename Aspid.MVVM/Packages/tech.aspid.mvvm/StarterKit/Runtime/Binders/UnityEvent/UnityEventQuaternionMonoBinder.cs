using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see cref="Quaternion"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(Quaternion))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Quaternion")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Quaternion")]
    public sealed partial class UnityEventQuaternionMonoBinder : MonoBinder, IRotationBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<Quaternion, Quaternion> _converter;

        [Tooltip("Invoked with the bound value.")]
        [SerializeField] private UnityEvent<Quaternion> _set;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(Quaternion value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
