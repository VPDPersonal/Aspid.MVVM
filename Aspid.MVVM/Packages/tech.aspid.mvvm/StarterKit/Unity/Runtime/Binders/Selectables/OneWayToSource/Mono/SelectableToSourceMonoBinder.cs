using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ComponentToSourceMonoBinder{Selectable}"/> that hands the ViewModel the
    /// <see cref="Selectable"/> this binder is attached to.
    /// </summary>
    /// <remarks>
    /// Eight domains had a ToSource binder and two did not; this is one of them. A ViewModel that has to select the
    /// control, or read its navigation, needs the reference — and reaching for it with
    /// <see cref="Component.GetComponent{T}"/> from the ViewModel is exactly the coupling the framework exists to
    /// remove.
    /// </remarks>
    [AddBinderContextMenu(typeof(Selectable))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable To Source Binder")]
    public sealed class SelectableToSourceMonoBinder : ComponentToSourceMonoBinder<Selectable> { }
}
