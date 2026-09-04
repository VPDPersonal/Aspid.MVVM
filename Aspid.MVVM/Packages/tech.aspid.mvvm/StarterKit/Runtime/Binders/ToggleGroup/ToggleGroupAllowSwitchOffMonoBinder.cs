using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="ToggleGroup.allowSwitchOff"/>.
    /// </summary>
    /// <remarks>
    /// Turning it off selects nothing: an empty group stays empty until the user presses a toggle.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(ToggleGroup), serializePropertyNames: "m_AllowSwitchOff")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ToggleGroup/ToggleGroup Binder – Allow Switch Off")]
    public class ToggleGroupAllowSwitchOffMonoBinder : ComponentMonoBinder<ToggleGroup, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.allowSwitchOff;
            set => CachedComponent.allowSwitchOff = value;
        }
    }
}
