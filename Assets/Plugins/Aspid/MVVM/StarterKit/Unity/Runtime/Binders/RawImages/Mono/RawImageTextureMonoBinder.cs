using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(RawImage), "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder - Texture")]
    [AddComponentContextMenu(typeof(RawImage),"Add RawImage Binder/RawImage Binder - Texture")]
    public partial class RawImageTextureMonoBinder : ComponentMonoBinder<RawImage>, IBinder<Texture2D>, IBinder<Sprite>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        [BinderLog]
        public void SetValue(Texture2D value)
        {
            CachedComponent.texture = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }

        [BinderLog]
        public void SetValue(Sprite value) =>
            SetValue(value?.texture);
    }
}