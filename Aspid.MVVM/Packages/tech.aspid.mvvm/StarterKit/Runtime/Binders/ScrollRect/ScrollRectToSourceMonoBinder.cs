using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="ScrollRect"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect To Source Binder")]
    public sealed class ScrollRectToSourceMonoBinder : ComponentToSourceMonoBinder<ScrollRect> { }
}
