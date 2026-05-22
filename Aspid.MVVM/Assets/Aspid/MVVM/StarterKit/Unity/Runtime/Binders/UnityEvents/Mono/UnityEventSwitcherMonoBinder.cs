using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{UnityEvent}"/> that invokes the selected <see cref="UnityEvent"/> based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Switcher")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Switcher")]
    public sealed class UnityEventSwitcherMonoBinder : SwitcherMonoBinder<UnityEvent>
    {
        /// <summary>
        /// Called when applying the selected value. Invokes the <see cref="UnityEvent"/> if it is not <see langword="null"/>.
        /// </summary>
        protected override void SetValue(UnityEvent value) =>
            value?.Invoke();
    }
}
