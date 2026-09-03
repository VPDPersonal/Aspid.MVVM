using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="TMP_Dropdown.alphaFadeSpeed"/>.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_AlphaFadeSpeed")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed")]
    public class DropdownAlphaFadeSpeedMonoBinder : ComponentFloatMonoBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.alphaFadeSpeed;
            set => CachedComponent.alphaFadeSpeed = this.NonNegative(value);
        }
    }
}
