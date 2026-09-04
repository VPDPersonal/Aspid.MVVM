using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="Scrollbar"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Scrollbar))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar To Source Binder")]
    public sealed class ScrollbarToSourceMonoBinder : ComponentToSourceMonoBinder<Scrollbar> { }
}
