using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="RawImage.texture"/>.
    /// </summary>
    /// <remarks>
    /// {R}
    /// </remarks>
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Enum")]
    public sealed class RawImageTextureEnumMonoBinder : EnumMonoBinder<RawImage, Texture>
    {
        [Tooltip("Disable the RawImage while the texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected override void SetValue(Texture value) =>
            CachedComponent.SetTexture(value, _disabledWhenNull);
    }
}
