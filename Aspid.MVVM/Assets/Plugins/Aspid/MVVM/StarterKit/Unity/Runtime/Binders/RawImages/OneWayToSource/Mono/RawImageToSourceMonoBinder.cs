using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="RawImage"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(RawImage))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage To Source Binder")]
    public sealed class RawImageToSourceMonoBinder : ComponentToSourceMonoBinder<RawImage> { }
}