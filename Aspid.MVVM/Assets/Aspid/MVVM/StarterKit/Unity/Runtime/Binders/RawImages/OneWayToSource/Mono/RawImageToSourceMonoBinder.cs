using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{RawImage}"/> that reads the current state of a <see cref="RawImage"/> component
    /// back to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(RawImage))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage To Source Binder")]
    public sealed class RawImageToSourceMonoBinder : ComponentToSourceMonoBinder<RawImage> { }
}