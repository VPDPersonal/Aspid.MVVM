using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Slider))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider To Source Binder")]
    public sealed class SliderToSourceMonoBinder : ComponentToSourceMonoBinder<Slider> { }
}