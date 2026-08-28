using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{HorizontalOrVerticalLayoutGroup}"/> that binds the <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property.
    /// </summary>
    /// <remarks>
    /// Also implements <see cref="IFloatBinder"/>: numeric ViewModel values are forwarded directly to
    /// <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/>. A non-finite value is rejected
    /// instead of being written.
    /// </remarks>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing")]
    public class HorizontalOrVerticalLayoutSpacingMonoBinder : ComponentFloatMonoBinder<HorizontalOrVerticalLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.spacing;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.spacing = value;
            }
        }
    }
}