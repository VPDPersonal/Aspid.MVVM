using UnityEngine;
using UnityEngine.Events;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see cref="Vector2"/> ViewModel value.
    /// </summary>
    [AddBinderContextMenuByType(typeof(Vector2))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Vector2")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Vector2")]
    public sealed partial class UnityEventVector2MonoBinder : MonoBinder, IBinder<Vector2>
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<Vector2> _set;

        /// <summary>
        /// Invokes the event with the specified <see cref="Vector2"/> value, applying the converter if configured.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Vector2 value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
