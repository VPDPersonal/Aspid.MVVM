using UnityEngine;
using UnityEngine.Events;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UnityEvent/UnityEvent Binder - Switcher")]
    public sealed class UnityEventSwitcherMonoBinder : SwitcherMonoBinder<UnityEvent>
    {
        protected override void SetValue(UnityEvent value) =>
            value?.Invoke();
    }
}