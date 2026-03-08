using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="Slider"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Slider))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider To Source Binder")]
    public sealed class SliderToSourceMonoBinder : ComponentToSourceMonoBinder<Slider> { }
}