using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{ 
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{LineRenderer}"/> that sends the cached <see cref="LineRenderer"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(LineRenderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer To Source Binder")]
    public sealed class LineRendererToSourceMonoBinder : ComponentToSourceMonoBinder<LineRenderer> { }
}