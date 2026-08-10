using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Substitutes an authored value for a <see langword="null"/> one.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <remarks>
    /// The reference-type counterpart of a placeholder string: a default sprite while an avatar
    /// loads, a neutral material for an unequipped slot. Without it the empty state has to be a
    /// second property on the ViewModel.
    /// <para>
    /// A destroyed <see cref="UnityEngine.Object"/> counts as missing here, which plain <c>??</c>
    /// would not catch: Unity's overloaded <c>==</c> reports a destroyed object as null while its
    /// managed reference is still alive, and a sprite destroyed mid-scene would otherwise reach the
    /// binder instead of the fallback.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Object", Name = "Null Coalesce", Tooltip = "Substitutes an authored value for a null one")]
    public sealed class NullCoalesceConverter<T> : IConverter<T?, T>
        where T : class
    {
        [Tooltip("Returned when the bound value is null.")]
        [SerializeField] private T _fallback = default!;

        public NullCoalesceConverter() { }

        /// <param name="fallback">Returned when the bound value is <see langword="null"/>.</param>
        public NullCoalesceConverter(T fallback)
        {
            _fallback = fallback;
        }

        /// <summary>
        /// Returns the specified value, or the fallback when it is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>The value, or the fallback.</returns>
        public T Convert(T? value)
        {
            // Deliberately Unity's overloaded ==: `is null` and `??` both report false for a
            // destroyed object, whose managed reference outlives the native one.
            if (value is UnityEngine.Object unityObject) return unityObject == null ? _fallback : value;

            return value ?? _fallback;
        }
    }
}
