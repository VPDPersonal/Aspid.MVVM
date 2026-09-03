using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="RendererPropertyBlockMonoBinder{TValue}"/> that writes a color shader property.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Color")]
    public partial class RendererPropertyBlockColorMonoBinder : RendererPropertyBlockMonoBinder<Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected override void Write(Color value) =>
            Block.SetColor(PropertyId, value);
    }
}
