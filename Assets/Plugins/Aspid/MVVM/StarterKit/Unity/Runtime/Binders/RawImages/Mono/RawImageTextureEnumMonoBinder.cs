using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(RawImage), "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder - Texture Enum")]
    [AddComponentContextMenu(typeof(LineRenderer),"Add RawImage Binder/RawImage Binder - Texture Enum")]
    public sealed class RawImageTextureEnumMonoBinder : EnumMonoBinder<RawImage, Texture2D>
    {
        [Header("Parameter")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Texture2D value)
        {
            CachedComponent.texture = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }
    }
}