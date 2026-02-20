using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Image))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image To Source Binder")]
    public sealed class ImageToSourceMonoBinder : ComponentToSourceMonoBinder<Image> { }
}