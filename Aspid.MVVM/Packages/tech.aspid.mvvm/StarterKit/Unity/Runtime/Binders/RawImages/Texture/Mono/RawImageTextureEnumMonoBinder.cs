using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinderWithConverter{T1, T2}"/> that sets the <see cref="RawImage.texture"/>
    /// property to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Enum")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "Enum")]
    public class RawImageTextureEnumMonoBinder : EnumMonoBinderWithConverter<RawImage, Texture>
    {
        [Tooltip("Disables the RawImage component when the bound texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <inheritdoc/>
        protected sealed override void SetValue(Texture value)
        {
            CachedComponent.texture = value;
            if (_disabledWhenNull) CachedComponent.enabled = value;
        }
    }
}