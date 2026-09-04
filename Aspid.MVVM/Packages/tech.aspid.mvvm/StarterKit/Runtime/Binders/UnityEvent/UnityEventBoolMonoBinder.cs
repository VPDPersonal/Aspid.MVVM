using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see langword="bool"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Bool")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Bool")]
    public sealed partial class UnityEventBoolMonoBinder : MonoBinder, IBinder<bool>
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        [Tooltip("Invoked with the bound value.")]
        [SerializeField] private UnityEvent<bool> _set;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(bool value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
