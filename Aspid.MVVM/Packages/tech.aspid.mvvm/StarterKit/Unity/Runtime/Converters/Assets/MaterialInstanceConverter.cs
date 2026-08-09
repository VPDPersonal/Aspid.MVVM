#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Hands a renderer its own copy of a material instead of the shared asset.
    /// </summary>
    /// <remarks>
    /// Writing to <c>Renderer.material</c> instantiates, but writing to a material a binder hands over
    /// does not — a per-object tint quietly edits the shared asset, project files included.
    /// <para>
    /// The copy is cached against the source, because a binder pushes on every notification and
    /// <c>new Material(...)</c> would leak one per push. The copies are owned here and released when
    /// the source changes.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Asset", Name = "Material Instance", Tooltip = "Hands a renderer its own copy of a material instead of the shared asset")]
    public sealed class MaterialInstanceConverter : IConverterMaterial
    {
        [Tooltip("Return a copy rather than the shared asset.")]
        [SerializeField] private bool _instantiate = true;

        [NonSerialized] private Material? _source;
        [NonSerialized] private Material? _copy;

        public MaterialInstanceConverter() { }

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
