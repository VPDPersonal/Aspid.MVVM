using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{ 
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Scrollbar}"/> that sends the cached <see cref="Scrollbar"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Scrollbar))]
    [AddComponentMenu("Aspid/MVVM/Binders/Scrollbar/Scrollbar To Source Binder")]
    public sealed class ScrollbarsToSourceMonoBinder : ComponentToSourceMonoBinder<Scrollbar> { }
}