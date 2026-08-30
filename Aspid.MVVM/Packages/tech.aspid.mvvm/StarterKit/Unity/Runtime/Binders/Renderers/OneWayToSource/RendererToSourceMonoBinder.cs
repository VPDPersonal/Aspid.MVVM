using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Renderer}"/> that sends the cached <see cref="Renderer"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer To Source Binder")]
    public sealed class RendererToSourceMonoBinder : ComponentToSourceMonoBinder<Renderer> { }
}