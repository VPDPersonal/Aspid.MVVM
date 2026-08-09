#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Hands a renderer its own copy of a material instead of the shared asset.
    /// </summary>
    /// <remarks>
    /// Writing to <c>Renderer.material</c> already instantiates, but writing to a material a binder
    /// hands over does not — so a per-object tint quietly edits the shared asset and every object
    /// using it changes, including in the project files. This is the point where that can be
    /// intercepted.
    /// <para>
    /// The copy is cached against the source, because a binder pushes on every notification and
    /// <c>new Material(...)</c> would leak one per push. The copies are owned by this converter and
    /// released when the source changes.
    /// </para>
    /// </remarks>
    [Serializable]
    public sealed class MaterialInstanceConverter : IConverterMaterial
    {
        [Tooltip("Return a copy rather than the shared asset.")]
        [SerializeField] private bool _instantiate = true;

        [NonSerialized] private Material? _source;
        [NonSerialized] private Material? _copy;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialInstanceConverter"/> class that copies.
        /// </summary>
        public MaterialInstanceConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialInstanceConverter"/> class.
        /// </summary>
        /// <param name="instantiate">Whether to return a copy rather than the shared asset.</param>
        public MaterialInstanceConverter(bool instantiate)
        {
            _instantiate = instantiate;
        }

        /// <summary>
        /// Returns a copy of the specified material.
        /// </summary>
        /// <param name="value">The material to copy.</param>
        /// <returns>
        /// A copy owned by this converter, reused while the source is unchanged, or the material
        /// itself when copying is off.
        /// </returns>
        public Material? Convert(Material? value)
        {
            if (!_instantiate || value == null)
            {
                Release();
                return value;
            }

            if (ReferenceEquals(_source, value) && _copy != null) return _copy;

            Release();

            _source = value;
            _copy = new Material(value) { name = value.name + " (Instance)" };

            return _copy;
        }

        private void Release()
        {
            if (_copy != null) UnityEngine.Object.Destroy(_copy);

            _copy = null;
            _source = null;
        }
    }
}
