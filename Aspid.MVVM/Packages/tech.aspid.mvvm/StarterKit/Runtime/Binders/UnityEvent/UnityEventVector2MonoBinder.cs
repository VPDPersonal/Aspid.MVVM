using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see cref="Vector2"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(Vector2))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Vector2")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Vector2")]
    public sealed partial class UnityEventVector2MonoBinder : MonoBinder, IVector2Binder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<Vector2, Vector2> _converter;

        [Tooltip("Invoked with the bound value.")]
        [SerializeField] private UnityEvent<Vector2> _set;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(Vector2 value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
