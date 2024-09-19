using UnityEngine;
using UnityEngine.UI;
using AspidUI.MVVM.Unity.Generation;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.RawImages
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture")]
    public partial class RawImageTextureMonoBinder : ComponentMonoBinder<RawImage>, IBinder<Texture2D>
    {
        [BinderLog]
        public void SetValue(Texture2D value) =>
            CachedComponent.texture = value;
    }
}