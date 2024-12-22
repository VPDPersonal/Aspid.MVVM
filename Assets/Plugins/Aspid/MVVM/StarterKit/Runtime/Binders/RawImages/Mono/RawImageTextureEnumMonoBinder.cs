using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Raw Image/RawImage Binder - Texture Enum")]
    public sealed class RawImageTextureEnumMonoBinder : EnumComponentMonoBinder<RawImage, Texture2D>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Texture2D value)
        {
            CachedComponent.texture = value;
            if (_disabledWhenNull) CachedComponent.enabled = value != null;
        }
    }
}