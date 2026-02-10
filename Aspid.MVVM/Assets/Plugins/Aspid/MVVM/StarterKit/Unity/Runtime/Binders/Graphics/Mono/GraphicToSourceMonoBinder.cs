using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Graphic))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic To Source Binder")]
    public sealed class GraphicToSourceMonoBinder : ComponentToSourceMonoBinder<Graphic> { }
}