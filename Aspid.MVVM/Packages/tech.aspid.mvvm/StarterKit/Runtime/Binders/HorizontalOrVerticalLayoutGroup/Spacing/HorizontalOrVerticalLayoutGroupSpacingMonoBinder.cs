using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds
    /// <see cref="HorizontalOrVerticalLayoutGroup.spacing"/>.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/HorizontalOrVerticalLayoutGroup/HorizontalOrVerticalLayoutGroup Binder – Spacing")]
    public class HorizontalOrVerticalLayoutGroupSpacingMonoBinder
        : ComponentFloatMonoBinder<HorizontalOrVerticalLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.spacing;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.spacing = value;
            }
        }
    }
}
