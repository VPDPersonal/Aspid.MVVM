using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{VirtualizedList}"/> that sends the current bound property value
    /// of a <see cref="VirtualizedList"/> back to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(VirtualizedList))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/VirtualizedList/VirtualizedList To Source Binder")]
    public sealed class VirtualizedListToSourceMonoBinder : ComponentToSourceMonoBinder<VirtualizedList> { }
}