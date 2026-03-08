using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture, UnityEngine.Texture>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterTexture;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="RawImage.texture"/> property on a <see cref="RawImage"/> component
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// Optionally disables the <see cref="RawImage"/> when the bound texture is <see langword="null"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture")]
    public sealed partial class RawImageTextureMonoBinder : ComponentMonoBinder<RawImage, Texture, Converter>, IBinder<Sprite>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override Texture Property
        {
            get => CachedComponent.texture;
            set
            {
                CachedComponent.texture = value;
                CachedComponent.enabled = !_disabledWhenNull || value;
            }
        }

        [BinderLog]
        public void SetValue(Sprite value) =>
            SetValue(value?.texture);
    }
}