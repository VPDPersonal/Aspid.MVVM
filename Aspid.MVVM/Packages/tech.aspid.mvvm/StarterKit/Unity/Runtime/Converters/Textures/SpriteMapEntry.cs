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
        /// The key the sprite is looked up by.
        /// </summary>
        [Tooltip("The key the sprite is looked up by.")]
        public string Key;

        /// <summary>
        /// The sprite that key names. Leave it empty to map a key to nothing.
        /// </summary>
        [Tooltip("The sprite that key names. Leave it empty to map a key to nothing.")]
        public Sprite? Sprite;
    }
}
