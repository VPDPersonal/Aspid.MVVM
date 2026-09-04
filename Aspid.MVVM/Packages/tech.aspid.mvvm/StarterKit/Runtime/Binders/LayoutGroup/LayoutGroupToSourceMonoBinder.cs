using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="LayoutGroup"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(LayoutGroup))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup To Source Binder")]
    public sealed class LayoutGroupToSourceMonoBinder : ComponentToSourceMonoBinder<LayoutGroup> { }
}
