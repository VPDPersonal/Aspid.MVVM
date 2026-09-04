using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="RawImage.texture"/> on each element.
    /// </summary>
    /// <remarks>
    /// {R}
    /// </remarks>
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture EnumGroup")]
    public sealed class RawImageTextureEnumGroupMonoBinder : EnumGroupMonoBinder<RawImage, Texture>
    {
        [Tooltip("Disable the RawImage while the texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected override void SetValue(RawImage element, Texture value) =>
            element.SetTexture(value, _disabledWhenNull);
    }
}
