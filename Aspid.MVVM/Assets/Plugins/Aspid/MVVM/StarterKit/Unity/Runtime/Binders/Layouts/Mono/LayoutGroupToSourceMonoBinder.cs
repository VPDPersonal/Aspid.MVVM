using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(LayoutGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup To Source Binder")]
    public sealed class LayoutGroupToSourceMonoBinder : ComponentToSourceMonoBinder<LayoutGroup> { }
}