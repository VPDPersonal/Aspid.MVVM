using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="RendererPropertyBlockMonoBinder{TValue}"/> that writes a vector shader property, also from
    /// <see cref="Vector2"/> and <see cref="Vector3"/>.
    /// </summary>
    /// <remarks>
    /// Missing components are written as zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Vector")]
    public partial class RendererPropertyBlockVectorMonoBinder : RendererPropertyBlockMonoBinder<Vector4>, IVectorBinder
    {
        /// <summary>
        /// Writes <paramref name="value"/> with Z and W set to zero.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Vector2 value) =>
            // Without base. the call would recurse into this overload instead of the Vector4 one.
            base.SetValue(value);

        /// <summary>
        /// Writes <paramref name="value"/> with W set to zero.
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
