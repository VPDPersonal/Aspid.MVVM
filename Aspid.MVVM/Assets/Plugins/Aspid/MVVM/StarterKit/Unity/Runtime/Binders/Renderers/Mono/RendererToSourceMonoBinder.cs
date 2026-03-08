using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="Renderer"/> component
    /// back to the ViewModel in one-way-to-source mode.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer To Source Binder")]
    public sealed class RendererToSourceMonoBinder : ComponentToSourceMonoBinder<Renderer> { }
}