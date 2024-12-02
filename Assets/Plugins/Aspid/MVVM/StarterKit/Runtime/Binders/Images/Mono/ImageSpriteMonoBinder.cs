using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite")]
    public partial class ImageSpriteMonoBinder : ComponentMonoBinder<Image>, IBinder<Sprite>
    {
        [Header("Parameter")]
        [SerializeField] private bool _disabledWhenNull = true;

        [BinderLog]
        public void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            if (_disabledWhenNull) CachedComponent.enabled = value != null;
        }
    }
}