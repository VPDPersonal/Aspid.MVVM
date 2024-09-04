using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity.Generation;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite")]
    public partial class ImageSpriteBinder : ComponentMonoBinder<Image>, IBinder<Sprite>
    {
        [BinderLog]
        public void SetValue(Sprite value) =>
            CachedComponent.sprite = value;
    }
}