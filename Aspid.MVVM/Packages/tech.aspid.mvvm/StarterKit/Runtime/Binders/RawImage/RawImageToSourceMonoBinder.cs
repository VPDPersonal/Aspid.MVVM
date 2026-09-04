using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="RawImage"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(RawImage))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage To Source Binder")]
    public sealed class RawImageToSourceMonoBinder : ComponentToSourceMonoBinder<RawImage> { }
}
