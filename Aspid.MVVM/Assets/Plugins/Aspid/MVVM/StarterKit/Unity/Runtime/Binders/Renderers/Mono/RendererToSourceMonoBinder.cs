using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer To Source Binder")]
    public sealed class RendererToSourceMonoBinder : ComponentToSourceMonoBinder<Renderer> { }
}