using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see langword="int"/>.
    /// </summary>
    /// <remarks>
    /// Also accepts the other numeric types.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(int))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Int")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Int")]
    public sealed partial class UnityEventIntMonoBinder : MonoBinder, IIntBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<int, int> _converter;

        [Tooltip("Invoked with the bound value.")]
        [SerializeField] private UnityEvent<int> _set;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(int value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
