using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that sets the <see cref="RawImage.texture"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture")]
    public sealed partial class RawImageTextureMonoBinder : ComponentMonoBinder<RawImage, Texture>, IBinder<Sprite>
    {
        [Tooltip("Disables the RawImage component when the bound texture is null.")]
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