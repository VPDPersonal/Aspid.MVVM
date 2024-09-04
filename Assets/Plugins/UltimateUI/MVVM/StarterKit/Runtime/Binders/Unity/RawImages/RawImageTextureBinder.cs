using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity.Generation;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.RawImages
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture")]
    public partial class RawImageTextureBinder : ComponentMonoBinder<RawImage>, IBinder<Texture2D>
    {
        [BinderLog]
        public void SetValue(Texture2D value) =>
            CachedComponent.texture = value;
    }
}