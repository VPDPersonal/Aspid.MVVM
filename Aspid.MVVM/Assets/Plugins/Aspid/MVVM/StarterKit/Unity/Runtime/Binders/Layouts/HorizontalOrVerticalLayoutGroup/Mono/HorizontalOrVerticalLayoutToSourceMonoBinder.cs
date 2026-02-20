using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup To Source Binder")]
    public sealed class HorizontalOrVerticalLayoutToSourceMonoBinder : ComponentToSourceMonoBinder<HorizontalOrVerticalLayoutGroup> { }
}