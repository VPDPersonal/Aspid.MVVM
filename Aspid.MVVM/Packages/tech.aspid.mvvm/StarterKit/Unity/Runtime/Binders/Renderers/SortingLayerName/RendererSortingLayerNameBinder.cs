#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Renderer, string}"/> that binds <see cref="Renderer.sortingLayerName"/>.
    /// </summary>
    /// <remarks>
    /// A name no layer has is refused with an error instead of being silently ignored by Unity.
    /// </remarks>
    [Serializable]
    public class RendererSortingLayerNameBinder : TargetBinder<Renderer, string>
    {
        /// <inheritdoc/>
        protected sealed override string Property
        {
            get => Target.sortingLayerName;
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (SortingLayer.NameToID(value) == 0 && value != "Default")
                {
                    this.LogError($"no sorting layer is named {value.Describe()}", "The layer is left unchanged.", Target);
                    return;
                }
                
                Target.sortingLayerName = value;
            }
        }

        /// <inheritdoc/>
        public RendererSortingLayerNameBinder(
            Renderer target,
            IConverter<string, string>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
