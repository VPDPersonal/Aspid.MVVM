using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="Selectable"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable To Source Binder")]
    public sealed class SelectableToSourceMonoBinder : ComponentToSourceMonoBinder<Selectable> { }
}
