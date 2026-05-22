using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{HorizontalOrVerticalLayoutGroup}"/> that sends the
    /// <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup"/> component reference back to the ViewModel
    /// when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup To Source Binder")]
    public sealed class HorizontalOrVerticalLayoutGroupToSourceMonoBinder : ComponentToSourceMonoBinder<HorizontalOrVerticalLayoutGroup> { }
}