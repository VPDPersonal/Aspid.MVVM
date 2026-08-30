using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{T1, T2}"/> that sets the <see cref="RawImage.texture"/> property
    /// on a group of <see cref="RawImage"/> components, applying the configured selected or default value to each entry
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture EnumGroup")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "EnumGroup")]
    public sealed class RawImageTextureEnumGroupMonoBinder : EnumGroupMonoBinder<RawImage, Texture>
    {
        [Tooltip("Disables each RawImage component when its resolved texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
     
        /// <inheritdoc/>
        protected override void SetValue(RawImage element, Texture value) 
        {
            element.texture = value;
            if (_disabledWhenNull) element.enabled = value;
        }
    }
}