using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that turns a bound <see langword="bool"/> into one of two configured values.
    /// </summary>
    /// <remarks>
    /// The two-state case that is not a colour block and not a sprite swap: a label that reads one word or another, a size
    /// that is either compact or full, a tint chosen in the Inspector rather than in code. Without it the ViewModel holds
    /// the presentation value itself, which is the wording and the look of the view leaking one layer down.
    /// <para/>
    /// The value is forwarded on every bound boolean, including the first, so the view starts in the state the ViewModel
    /// describes rather than in whatever the scene was authored with.
    /// </remarks>
    /// <typeparam name="TValue">The type of value chosen between.</typeparam>
    public abstract partial class ConditionalMonoBinder<TValue> : MonoBinder, IBinder<bool>
    {
        [Tooltip("Value forwarded when the bound value is true.")]
        [SerializeField] private TValue _whenTrue;

        [Tooltip("Value forwarded when the bound value is false.")]
        [SerializeField] private TValue _whenFalse;

        [Tooltip("Invoked with the chosen value each time the bound boolean arrives.")]
        [SerializeField] private UnityEvent<TValue> _value;

        /// <summary>
        /// Forwards the value configured for <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value) =>
            _value?.Invoke(value ? _whenTrue : _whenFalse);
    }
}
