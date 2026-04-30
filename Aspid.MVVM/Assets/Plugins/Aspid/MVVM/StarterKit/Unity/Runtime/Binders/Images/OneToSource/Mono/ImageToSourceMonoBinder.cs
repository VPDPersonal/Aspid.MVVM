using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Image}"/> that sends the cached <see cref="Image"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Image))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image To Source Binder")]
    public sealed class ImageToSourceMonoBinder : ComponentToSourceMonoBinder<Image> { }
}