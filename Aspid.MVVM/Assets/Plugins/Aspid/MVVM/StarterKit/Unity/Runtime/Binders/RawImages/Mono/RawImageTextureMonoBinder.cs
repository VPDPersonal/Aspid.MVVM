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