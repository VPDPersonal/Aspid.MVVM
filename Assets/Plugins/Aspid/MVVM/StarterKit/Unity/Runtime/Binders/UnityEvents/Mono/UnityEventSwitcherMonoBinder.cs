using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Switcher")]
    [AddComponentContextMenu(typeof(Component),"Add UnityEvent Binder/UnityEvent Binder - Switcher")]
    public sealed class UnityEventSwitcherMonoBinder : SwitcherMonoBinder<UnityEvent>
    {
        protected override void SetValue(UnityEvent value) =>
            value?.Invoke();
    }
}