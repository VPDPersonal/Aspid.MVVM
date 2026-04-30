using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture, UnityEngine.Texture>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterTexture;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{RawImage, Texture, Converter}"/> that switches the <see cref="RawImage.texture"/>
    /// property between two <see cref="Texture"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// Disables the <see cref="RawImage"/> component when the selected texture is <see langword="null"/> and
    /// the Disable When Null option is enabled.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Switcher")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "Switcher")]
    public sealed class RawImageTextureSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Texture, Converter>
    {
        [Tooltip("When true, disables the RawImage component automatically when the selected texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <inheritdoc/>
        protected override void SetValue(Texture value)
        {
            CachedComponent.texture = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }
    }
}