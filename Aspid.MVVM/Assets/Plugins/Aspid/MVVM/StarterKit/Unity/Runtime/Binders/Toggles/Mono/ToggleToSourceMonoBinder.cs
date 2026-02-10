using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Toggle))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle To Source Binder")]
    public sealed class ToggleToSourceMonoBinder : ComponentToSourceMonoBinder<Toggle> { }
}