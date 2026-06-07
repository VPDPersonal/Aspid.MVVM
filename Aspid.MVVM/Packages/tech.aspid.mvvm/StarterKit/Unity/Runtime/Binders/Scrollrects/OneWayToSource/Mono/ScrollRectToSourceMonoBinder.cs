using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{ 
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{ScrollRect}"/> that sends the cached <see cref="ScrollRect"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect))]
    [AddComponentMenu("Aspid/MVVM/Binders/ScrollRect/ScrollRect To Source Binder")]
    public sealed class ScrollRectToSourceMonoBinder : ComponentToSourceMonoBinder<ScrollRect> { }
}