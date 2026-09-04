using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="HorizontalOrVerticalLayoutGroup"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/HorizontalOrVerticalLayoutGroup/HorizontalOrVerticalLayoutGroup To Source Binder")]
    public sealed class HorizontalOrVerticalLayoutGroupToSourceMonoBinder
        : ComponentToSourceMonoBinder<HorizontalOrVerticalLayoutGroup> { }
}
