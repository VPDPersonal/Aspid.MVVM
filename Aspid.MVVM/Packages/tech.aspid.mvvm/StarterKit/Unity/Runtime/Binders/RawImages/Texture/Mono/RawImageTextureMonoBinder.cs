using UnityEngine;
using UnityEngine.UI;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture, UnityEngine.Texture>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{RawImage, Texture, Converter}"/> that sets the <see cref="RawImage.texture"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current value
    /// is sent back to the ViewModel.
    /// Disables the <see cref="RawImage"/> component when the bound texture is <see langword="null"/> and
    /// the Disable When Null option is enabled.
    /// </remarks>
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture")]
    public sealed partial class RawImageTextureMonoBinder : ComponentMonoBinder<RawImage, Texture, Converter>, IBinder<Sprite>
    {
        [Tooltip("When true, disables the RawImage component automatically when the bound texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <inheritdoc/>
        protected override Texture Property
        {
            get => CachedComponent.texture;
            set
            {
                CachedComponent.texture = value;
                if (_disabledWhenNull) CachedComponent.enabled = value;
            }
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(Sprite value) =>
            SetValue(value?.texture);
    }
}