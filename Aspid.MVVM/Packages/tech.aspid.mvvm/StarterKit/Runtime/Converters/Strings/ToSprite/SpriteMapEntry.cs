#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// One key of a <see cref="StringToSpriteConverter"/> map, with the sprite it names.
    /// </summary>
    [Serializable]
    public struct SpriteMapEntry
    {
        /// <summary>
        /// Gets the key the sprite is looked up by.
        /// </summary>
        [field: Tooltip("The key the sprite is looked up by.")]
        [field: SerializeField]
        public string Key { get; private set; }

        /// <summary>
        /// Gets the sprite <see cref="Key"/> names, or <see langword="null"/> when the key maps to nothing.
        /// </summary>
        [field: Tooltip("The sprite that key names. Leave it empty to map a key to nothing.")]
        [field: SerializeField]
        public Sprite? Sprite { get; private set; }

        /// <param name="key">The key the sprite is looked up by.</param>
        /// <param name="sprite">
        /// The sprite that key names, or <see langword="null"/> to map the key to nothing.
        /// </param>
        public SpriteMapEntry(
            string key,
            Sprite? sprite)
        {
            Key = key;
            Sprite = sprite;
        }
    }
}
