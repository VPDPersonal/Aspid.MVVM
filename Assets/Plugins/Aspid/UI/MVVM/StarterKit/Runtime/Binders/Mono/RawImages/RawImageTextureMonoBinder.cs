using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.RawImages
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture")]
    public partial class RawImageTextureMonoBinder : ComponentMonoBinder<RawImage>, IBinder<Texture2D>
    {
        [BinderLog]
        public void SetValue(Texture2D value) =>
            CachedComponent.texture = value;
    }
}