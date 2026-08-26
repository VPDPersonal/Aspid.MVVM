#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Hands a renderer its own copy of a material instead of the shared asset.
    /// </summary>
    /// <remarks>
    /// The copy is owned by the converter: it is cached while the source is unchanged and destroyed
    /// when the source changes — creating one per push would leak.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Material",
        Name = "Material Instance",
        Tooltip = "Hands a renderer its own copy of a material instead of the shared asset")]
    public sealed class MaterialInstanceConverter : IConverter<Material?, Material?>
    {
        [Tooltip("Return a copy rather than the shared asset.")]
        [SerializeField] private bool _instantiate = true;

        [NonSerialized] private Material? _source;
        [NonSerialized] private Material? _copy;

        /// <remarks>Default: handing out a copy.</remarks>
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
        /// A copy owned by this converter, reused while the source is unchanged; the material itself
        /// when copying is off; or <see langword="null"/> when the material is missing or destroyed.
        /// The previously returned copy is destroyed on the way, so a caller holding on to it is left
        /// with nothing.
        /// </returns>
        public Material? Convert(Material? value)
        {
            if (!_instantiate || value == null)
            {
                Release();
                return value;
            }

            if (ReferenceEquals(_source, value) && _copy != null)
                return _copy;

            Release();

            _source = value;
            _copy = new Material(value)
            {
                name = value.name + " (Instance)"
            };

            return _copy;
        }

        private void Release()
        {
            if (_copy != null)
                Object.Destroy(_copy);

            _copy = null;
            _source = null;
        }
    }
}
