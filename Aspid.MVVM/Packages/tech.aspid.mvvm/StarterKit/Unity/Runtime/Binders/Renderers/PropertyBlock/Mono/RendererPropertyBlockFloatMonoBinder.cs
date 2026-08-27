using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="RendererPropertyBlockMonoBinder{T}">RendererPropertyBlockMonoBinder&lt;float&gt;</see> that
    /// also implements <see cref="INumberBinder"/>, writing a <see langword="float"/> shader property.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Float")]
    public partial class RendererPropertyBlockFloatMonoBinder : RendererPropertyBlockMonoBinder<float>, INumberBinder
    {
        /// <summary>
        /// Casts the value to <see langword="float"/> and writes it.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        [BinderLog]
        public void SetValue(long value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        /// <remarks>
        /// Narrowed to <see langword="float"/> — precision may be lost.
        /// </remarks>
        [BinderLog]
        public void SetValue(double value) => SetValue((float)value);

        /// <inheritdoc/>
        protected override void Write(float value) =>
            Block.SetFloat(PropertyId, value);
    }
}
