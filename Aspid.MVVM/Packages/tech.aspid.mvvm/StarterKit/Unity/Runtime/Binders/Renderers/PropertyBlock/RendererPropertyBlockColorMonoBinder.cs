using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="RendererPropertyBlockMonoBinder{T}">RendererPropertyBlockMonoBinder&lt;Color&gt;</see> that
    /// writes a colour shader property, and accepts a hex string as well.
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
