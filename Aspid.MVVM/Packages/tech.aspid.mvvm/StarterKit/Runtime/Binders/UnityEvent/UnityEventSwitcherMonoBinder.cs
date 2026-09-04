using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{T}"/> that invokes one of two <see cref="UnityEvent"/>s by the bound
    /// <see langword="bool"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Switcher")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Switcher")]
    public sealed class UnityEventSwitcherMonoBinder : SwitcherMonoBinder<UnityEvent>
    {
        /// <inheritdoc/>
        protected override void SetValue(UnityEvent value) =>
            value?.Invoke();
    }
}
