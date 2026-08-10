#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Looks a sprite up by name.
    /// </summary>
    /// <remarks>
    /// The item catalogue case: a backend sends <c>"sword_iron"</c> and the icon lives in the
    /// project. The ViewModel would otherwise have to hold a <see cref="Sprite"/> — a reference to a
    /// project asset in a layer that is meant not to know about them — or the scene would need one
    /// switcher binder per icon.
    /// <para>
    /// The lookup is a scan of the authored array rather than a dictionary. The maps are written by
    /// hand and are short, the scan allocates nothing, and a dictionary built once from the array
    /// would go stale the moment the array is edited in play mode — which is exactly when someone is
    /// looking at it.
    /// </para>
    /// <para>
    /// A <c>SpriteAtlas</c> is deliberately not a source here: <c>SpriteAtlas.GetSprite</c> returns a
    /// fresh <see cref="Sprite"/> instance on every call, so a binder pushing on every notification
    /// would leak one per push, and a cache keyed by string would grow without a bound. Point the
    /// map at the sprites instead.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Texture", Name = "String To Sprite", Tooltip = "Looks a sprite up by name")]
    public sealed class StringToSpriteConverter : IConverter<string?, Sprite?>
    {
        [Tooltip("The keys and the sprites they name.")]
        [SerializeField] private SpriteMapEntry[] _map = Array.Empty<SpriteMapEntry>();

        [Tooltip("Match keys without regard to case.")]
        [SerializeField] private bool _ignoreCase;

        [Tooltip("Used when the key is blank, and when nothing matches it.")]
        [SerializeField] private Sprite? _fallback;

        [Tooltip("What to do with a key the map does not hold. ReturnInput is not available here — "
            + "the input is a string and the output a sprite — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [NonSerialized] private bool _loggedFailure;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToSpriteConverter"/> class with an empty map.
        /// </summary>
        public StringToSpriteConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToSpriteConverter"/> class.
        /// </summary>
        /// <param name="map">The keys and the sprites they name.</param>
        /// <param name="fallback">Used when the key is blank, and when nothing matches it.</param>
        /// <param name="ignoreCase">Whether to match keys without regard to case.</param>
        public StringToSpriteConverter(SpriteMapEntry[]? map, Sprite? fallback = null, bool ignoreCase = false)
        {
            _map = map ?? Array.Empty<SpriteMapEntry>();
            _fallback = fallback;
            _ignoreCase = ignoreCase;
        }

        /// <summary>
        /// Looks up the sprite the specified key names.
        /// </summary>
        /// <param name="value">The key to look up.</param>
        /// <returns>
        /// The sprite mapped to the key, or the fallback. A blank key is treated as no value rather
        /// than as a failed lookup and returns the fallback silently.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the map does not hold the key and <c>_onFailure</c> is
        /// <see cref="ConverterFailureMode.Throw"/>.
        /// </exception>
        public Sprite? Convert(string? value)
        {
            if (string.IsNullOrEmpty(value)) return _fallback;

            if (_map is { Length: > 0 })
            {
                var comparison = _ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

                for (var i = 0; i < _map.Length; i++)
                    if (string.Equals(_map[i].Key, value, comparison))
                        return _map[i].Sprite;
            }

            if (_onFailure is ConverterFailureMode.Throw)
                throw new ArgumentException($"No sprite is mapped to \"{value}\".", nameof(value));

            LogFailure(value);
            return _fallback;
        }

        private void LogFailure(string? value)
        {
            if (_loggedFailure) return;
            _loggedFailure = true;

            Debug.LogError(
                $"{nameof(StringToSpriteConverter)}: no sprite is mapped to \"{value}\". "
                + "Using the fallback sprite. Further failures on this converter are not reported.");
        }
    }
}
