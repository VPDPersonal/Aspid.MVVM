using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite")]
    public partial class ImageSpriteMonoBinder : ComponentMonoBinder<Image>, IBinder<Sprite>
    {
        [BinderLog]
        public void SetValue(Sprite value) =>
            CachedComponent.sprite = value;
    }
}