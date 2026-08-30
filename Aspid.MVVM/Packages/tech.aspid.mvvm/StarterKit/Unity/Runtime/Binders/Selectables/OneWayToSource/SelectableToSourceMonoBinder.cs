using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ComponentToSourceMonoBinder{Selectable}"/> that hands the ViewModel the
    /// <see cref="Selectable"/> this binder is attached to.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable To Source Binder")]
    public sealed class SelectableToSourceMonoBinder : ComponentToSourceMonoBinder<Selectable> { }
}
