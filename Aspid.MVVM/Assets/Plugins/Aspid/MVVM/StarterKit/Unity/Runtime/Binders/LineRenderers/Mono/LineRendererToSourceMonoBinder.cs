using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(LineRenderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer To Source Binder")]
    public sealed class LineRendererToSourceMonoBinder : ComponentToSourceMonoBinder<LineRenderer> { }
}