using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Slider}"/> that sends the cached <see cref="Slider"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Slider))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider To Source Binder")]
    public sealed class SliderToSourceMonoBinder : ComponentToSourceMonoBinder<Slider> { }
}