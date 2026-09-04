using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="RawImage.texture"/>.
    /// </summary>
    /// <remarks>
    /// {R}
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Switcher")]
    public sealed class RawImageTextureSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Texture>
    {
        [Tooltip("Disable the RawImage while the texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected override void SetValue(Texture value) =>
            CachedComponent.SetTexture(value, _disabledWhenNull);
    }
}
