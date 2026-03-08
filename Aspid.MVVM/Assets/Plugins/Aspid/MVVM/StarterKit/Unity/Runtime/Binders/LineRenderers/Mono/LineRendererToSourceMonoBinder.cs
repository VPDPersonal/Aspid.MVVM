using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="LineRenderer"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(LineRenderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer To Source Binder")]
    public sealed class LineRendererToSourceMonoBinder : ComponentToSourceMonoBinder<LineRenderer> { }
}