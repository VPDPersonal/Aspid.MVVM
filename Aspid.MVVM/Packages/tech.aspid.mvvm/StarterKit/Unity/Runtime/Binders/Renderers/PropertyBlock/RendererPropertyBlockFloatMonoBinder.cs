using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="RendererPropertyBlockMonoBinder{T}">RendererPropertyBlockMonoBinder&lt;float&gt;</see> that
    /// also implements <see cref="IFloatBinder"/>, writing a <see langword="float"/> shader property.
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
