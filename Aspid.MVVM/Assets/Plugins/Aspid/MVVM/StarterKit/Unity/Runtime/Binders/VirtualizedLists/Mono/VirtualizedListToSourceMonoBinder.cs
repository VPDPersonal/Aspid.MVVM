using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(VirtualizedList))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/VirtualizedList/VirtualizedList To Source Binder")]
    public sealed class VirtualizedListToSourceMonoBinder : ComponentToSourceMonoBinder<VirtualizedList> { }
}