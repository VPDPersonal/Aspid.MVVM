using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="RendererPropertyBlockMonoBinder{TValue}"/> that writes a float shader property.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Float")]
    public partial class RendererPropertyBlockFloatMonoBinder : RendererPropertyBlockMonoBinder<float>, IFloatBinder
    {
        /// <inheritdoc/>
        protected override void Write(float value) =>
            Block.SetFloat(PropertyId, value);
    }
}
