using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(RawImage), "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder - Texture Switcher")]
    [AddComponentContextMenu(typeof(LineRenderer),"Add RawImage Binder/RawImage Binder - Texture Switcher")]
    public sealed class RawImageTextureSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Texture2D>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Texture2D value)
        {
            CachedComponent.texture = value;
            if (_disabledWhenNull) CachedComponent.enabled = value != null;
        }
    }
}