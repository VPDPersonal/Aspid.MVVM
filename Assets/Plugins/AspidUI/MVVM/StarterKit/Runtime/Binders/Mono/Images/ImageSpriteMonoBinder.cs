using UnityEngine;
using UnityEngine.UI;
using AspidUI.MVVM.Unity.Generation;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite")]
    public partial class ImageSpriteMonoBinder : ComponentMonoBinder<Image>, IBinder<Sprite>
    {
        [BinderLog]
        public void SetValue(Sprite value) =>
            CachedComponent.sprite = value;
    }
}