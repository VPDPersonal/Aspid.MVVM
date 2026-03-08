using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of an <see cref="Image"/> component
    /// back to the ViewModel in one-way-to-source mode.
    /// </summary>
    [AddBinderContextMenu(typeof(Image))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image To Source Binder")]
    public sealed class ImageToSourceMonoBinder : ComponentToSourceMonoBinder<Image> { }
}