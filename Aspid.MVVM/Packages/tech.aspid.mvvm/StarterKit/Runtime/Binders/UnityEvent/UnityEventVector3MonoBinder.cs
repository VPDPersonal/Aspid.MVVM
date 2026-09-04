using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see cref="Vector3"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(Vector3))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Vector3")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Vector3")]
    public sealed partial class UnityEventVector3MonoBinder : MonoBinder, IVector3Binder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<Vector3, Vector3> _converter;

        [Tooltip("Invoked with the bound value.")]
        [SerializeField] private UnityEvent<Vector3> _set;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(Vector3 value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
