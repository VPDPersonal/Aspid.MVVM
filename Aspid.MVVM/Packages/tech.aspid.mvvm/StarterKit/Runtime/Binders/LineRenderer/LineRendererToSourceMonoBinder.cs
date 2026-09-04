using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="LineRenderer"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(LineRenderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer To Source Binder")]
    public sealed class LineRendererToSourceMonoBinder : ComponentToSourceMonoBinder<LineRenderer> { }
}
