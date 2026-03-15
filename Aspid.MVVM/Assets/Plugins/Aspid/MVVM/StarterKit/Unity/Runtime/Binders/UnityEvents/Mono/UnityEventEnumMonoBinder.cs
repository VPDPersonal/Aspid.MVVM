using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{UnityEvent}"/> that invokes the <see cref="UnityEvent"/> mapped to the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Enum")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Enum")]
    public sealed class UnityEventEnumMonoBinder : EnumMonoBinder<UnityEvent>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value. Invokes the mapped <see cref="UnityEvent"/> if it is not <see langword="null"/>.
        /// </summary>
        protected override void SetValue(UnityEvent value) =>
            value?.Invoke();
    }
}
