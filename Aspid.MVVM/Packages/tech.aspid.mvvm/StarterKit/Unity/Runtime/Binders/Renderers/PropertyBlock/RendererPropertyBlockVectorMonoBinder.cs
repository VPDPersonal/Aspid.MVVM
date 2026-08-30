using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="RendererPropertyBlockMonoBinder{T}">RendererPropertyBlockMonoBinder&lt;Vector4&gt;</see> that
    /// writes a vector shader property, and accepts <see cref="Vector2"/> and <see cref="Vector3"/> as well.
    /// </summary>
    /// <remarks>
    /// Shader vectors are always four components; a <see cref="Vector2"/> or <see cref="Vector3"/> arrives with the
    /// remaining components left at zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Vector")]
    public partial class RendererPropertyBlockVectorMonoBinder : RendererPropertyBlockMonoBinder<Vector4>, IVectorBinder
    {
        /// <summary>
        /// Writes <paramref name="value"/> with Z and W left at zero.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Vector2 value) =>
            // Without base., this would recurse into itself instead of the Vector4 overload.
            base.SetValue(value);

        /// <summary>
        /// Writes <paramref name="value"/> with W left at zero.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Vector3 value) =>
            base.SetValue(value);

        /// <inheritdoc/>
        protected override void Write(Vector4 value) =>
            Block.SetVector(PropertyId, value);
    }
}
