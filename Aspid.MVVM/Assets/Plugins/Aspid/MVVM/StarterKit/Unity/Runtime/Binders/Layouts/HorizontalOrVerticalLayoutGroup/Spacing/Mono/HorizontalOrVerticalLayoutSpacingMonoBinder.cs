using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{HorizontalOrVerticalLayoutGroup}"/> that binds the <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current spacing value
    /// is sent back to the ViewModel.
    /// Also implements <see cref="INumberBinder"/>: numeric ViewModel values are forwarded directly to
    /// <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing")]
    public class HorizontalOrVerticalLayoutSpacingMonoBinder : ComponentFloatMonoBinder<HorizontalOrVerticalLayoutGroup>, INumberBinder
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.spacing;
            set => CachedComponent.spacing = value;
        }
    }
}