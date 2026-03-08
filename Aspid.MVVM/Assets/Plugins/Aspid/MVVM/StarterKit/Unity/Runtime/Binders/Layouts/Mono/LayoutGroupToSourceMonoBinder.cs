using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="UnityEngine.UI.LayoutGroup"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(LayoutGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup To Source Binder")]
    public sealed class LayoutGroupToSourceMonoBinder : ComponentToSourceMonoBinder<LayoutGroup> { }
}