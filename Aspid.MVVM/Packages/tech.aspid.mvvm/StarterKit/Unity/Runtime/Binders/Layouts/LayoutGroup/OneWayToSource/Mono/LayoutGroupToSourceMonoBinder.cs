using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{LayoutGroup}"/> that sends the
    /// <see cref="UnityEngine.UI.LayoutGroup"/> component reference back to the ViewModel
    /// when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(LayoutGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup To Source Binder")]
    public sealed class LayoutGroupToSourceMonoBinder : ComponentToSourceMonoBinder<LayoutGroup> { }
}