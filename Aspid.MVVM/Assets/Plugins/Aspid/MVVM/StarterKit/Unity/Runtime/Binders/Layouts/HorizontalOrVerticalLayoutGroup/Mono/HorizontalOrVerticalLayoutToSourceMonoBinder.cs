using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup To Source Binder")]
    public sealed class HorizontalOrVerticalLayoutToSourceMonoBinder : ComponentToSourceMonoBinder<HorizontalOrVerticalLayoutGroup> { }
}