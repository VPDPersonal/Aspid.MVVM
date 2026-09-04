using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="VirtualizedList"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(VirtualizedList))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/VirtualizedList/VirtualizedList To Source Binder")]
    public sealed class VirtualizedListToSourceMonoBinder : ComponentToSourceMonoBinder<VirtualizedList> { }
}
