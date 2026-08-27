using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that parses a bound
    /// <see cref="string"/> as a <see langword="float"/> and forwards the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// A string that does not parse forwards the fallback value, and so does one that parses to <c>NaN</c> or an
    /// infinity — those are words float parsing accepts, and a clamp downstream cannot stop them. Failures are not
    /// logged by default: a half-typed number is normal while a user is typing, and an error per keystroke would bury
    /// the console.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/String To Float Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/String To Float Caster Binder")]
    public sealed partial class StringToFloatCasterMonoBinder : MonoBinder, IBinder<string>
    {
        [Tooltip("Invoked with the parsed value.")]
        [SerializeField] private UnityEvent<float> _casted;

        [Tooltip("Value forwarded when the string cannot be parsed.")]
        [SerializeField] private float _fallback;

        [Tooltip("Logs an error for every string that fails to parse.")]
        [SerializeField] private bool _logFailures;

        /// <summary>
        /// Parses <paramref name="value"/> and invokes the target <see cref="UnityEvent{T}"/> with the result, or with
        /// the fallback value when it does not parse.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            if (StringNumberParse.TryFloat(value, out var parsed))
            {
                _casted?.Invoke(parsed);
                return;
            }

            if (_logFailures)
                Debug.LogError($"[{nameof(StringToFloatCasterMonoBinder)}] '{value}' is not a finite number; forwarding {_fallback}.", context: this);

            _casted?.Invoke(_fallback);
        }
    }
}
