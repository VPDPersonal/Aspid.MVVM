using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Image.sprite"/> property on an <see cref="Image"/> component
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// Optionally disables the <see cref="Image"/> when the bound sprite is <see langword="null"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite")]
    public partial class ImageSpriteMonoBinder : ComponentMonoBinder<Image, Sprite>, IBinder<Texture2D>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected sealed override Sprite Property
        {
            get => CachedComponent.sprite;
            set
            {
                CachedComponent.sprite = value;
                CachedComponent.enabled = !_disabledWhenNull || value;
            }
        }

        [BinderLog]
        public void SetValue(Texture2D value)
        {
            var sprite = !value 
                ? null 
                : Sprite.Create(value, new Rect(0, 0, value.width, value.height), new Vector2(0.5f, 0.5f));
            
            SetValue(sprite);
        }
    }
}