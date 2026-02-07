using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder – Texture Enum")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "Enum")]
    public sealed class RawImageTextureEnumMonoBinder : EnumMonoBinder<RawImage, Texture2D>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Texture2D value)
        {
            CachedComponent.texture = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }
    }
}