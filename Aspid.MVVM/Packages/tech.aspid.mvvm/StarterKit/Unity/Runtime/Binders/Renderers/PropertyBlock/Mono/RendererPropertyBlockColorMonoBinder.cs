using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="RendererPropertyBlockMonoBinder{T}">RendererPropertyBlockMonoBinder&lt;Color&gt;</see> that
    /// writes a colour shader property.
    /// </summary>
    /// <remarks>
    /// A team colour, a rarity tint or a highlight per object, without a material per object.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Color")]
    public partial class RendererPropertyBlockColorMonoBinder : RendererPropertyBlockMonoBinder<Color>
    {
        /// <inheritdoc/>
        protected override void Write(Color value) =>
            Block.SetColor(PropertyId, value);
    }
}
