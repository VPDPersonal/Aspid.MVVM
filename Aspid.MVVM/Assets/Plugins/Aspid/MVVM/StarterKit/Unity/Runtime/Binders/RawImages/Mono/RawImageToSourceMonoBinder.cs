using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(RawImage))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage To Source Binder")]
    public sealed class RawImageToSourceMonoBinder : ComponentToSourceMonoBinder<RawImage> { }
}