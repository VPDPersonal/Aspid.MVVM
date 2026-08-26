#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Looks a sprite up by name.
    /// </summary>
    /// <remarks>
    /// A <c>SpriteAtlas</c> is deliberately not a source: <c>SpriteAtlas.GetSprite</c> returns a fresh
    /// <see cref="Sprite"/> on every call, so a binder pushing per notification would leak one per push.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Sprite",
        Name = "To Sprite",
        Tooltip = "Looks a sprite up by name")]
    public sealed class StringToSpriteConverter : IConverter<string?, Sprite?>
    {
        [Tooltip("The keys and the sprites they name.")]
        [SerializeField] private SpriteMapEntry[] _map = Array.Empty<SpriteMapEntry>();

        [Tooltip("Match keys without regard to case.")]
        [SerializeField] private bool _ignoreCase;

        [Tooltip("Used when the key is blank, spaces included, or matches nothing.")]
        [SerializeField] private Sprite? _fallback;

        /// <remarks>Default: an empty map, so every key falls back to <see langword="null"/>.</remarks>
        public StringToSpriteConverter() { }

        /// <param name="map">The keys and the sprites they name.</param>
        /// <param name="fallback">
        /// Used when the key is blank — a key of only spaces counts — and when nothing matches it.
        /// When omitted, returns <see langword="null"/>.
        /// </param>
        /// <param name="ignoreCase">Whether to match keys without regard to case.</param>
        public StringToSpriteConverter(SpriteMapEntry[]? map, Sprite? fallback = null, bool ignoreCase = false)
        {
            _fallback = fallback;
            _ignoreCase = ignoreCase;
            _map = map ?? Array.Empty<SpriteMapEntry>();
        }

        /// <summary>
        /// Looks up the sprite the specified key names.
        /// </summary>
        /// <param name="value">The key to look up.</param>
        /// <returns>
        /// The sprite mapped to the key, or the fallback. A blank key, spaces included, is treated as
        /// no value rather than as a failed lookup and returns the fallback silently.
        /// </returns>
        public Sprite? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            if (_map is { Length: > 0 })
            {
                var comparison = _ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

                foreach (var entry in _map)
                {
                    if (string.Equals(entry.Key, value, comparison))
                        return entry.Sprite;
                }
            }

            return this.UseFallback(
                fallback: _fallback,
                problem: value.Expected("a key the map holds"));
        }
    }
}
