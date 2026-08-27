using UnityEngine;
using UnityEngine.UI;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture, UnityEngine.Texture>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{RawImage, Texture, Converter}"/> that switches the <see cref="RawImage.texture"/>
    /// property between two <see cref="Texture"/> values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Switcher")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "Switcher")]
    public sealed class RawImageTextureSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Texture, Converter>
    {
        [Tooltip("Disables the RawImage component when the selected texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <inheritdoc/>
        protected override void SetValue(Texture value)
        {
            CachedComponent.texture = value;
            if (_disabledWhenNull) CachedComponent.enabled = value;
        }
    }
}