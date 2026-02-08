using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [AddBinderContextMenu(typeof(MonoView))]
    [AddComponentMenu("Aspid/MVVM/Binders/Views/ScriptableView Binder")]
    public class ScriptableViewMonoBinder : MonoViewMonoBinder<ScriptableView> { }
}