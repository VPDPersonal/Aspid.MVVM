using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Enum")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Enum")]
    public sealed class UnityEventEnumMonoBinder : EnumMonoBinder<UnityEvent>
    {
        protected override void SetValue(UnityEvent value) =>
            value?.Invoke();
    }
}