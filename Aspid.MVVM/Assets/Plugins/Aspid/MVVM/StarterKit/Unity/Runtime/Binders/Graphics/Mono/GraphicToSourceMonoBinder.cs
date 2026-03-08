using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads <see cref="Graphic"/> properties back to the ViewModel in one-way-to-source mode.
    /// </summary>
    [AddBinderContextMenu(typeof(Graphic))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic To Source Binder")]
    public sealed class GraphicToSourceMonoBinder : ComponentToSourceMonoBinder<Graphic> { }
}