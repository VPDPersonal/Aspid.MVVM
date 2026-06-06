using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Graphic}"/> that sends the cached <see cref="Graphic"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Graphic))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic To Source Binder")]
    public sealed class GraphicToSourceMonoBinder : ComponentToSourceMonoBinder<Graphic> { }
}