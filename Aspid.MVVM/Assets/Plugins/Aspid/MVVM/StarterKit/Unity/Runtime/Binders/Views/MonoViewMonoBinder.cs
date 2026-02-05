using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(MonoView))]
    [AddComponentMenu("Aspid/MVVM/Binders/Views/Mono View Binder")]
    public class MonoViewMonoBinder : ViewMonoBinder<MonoView> { }
}