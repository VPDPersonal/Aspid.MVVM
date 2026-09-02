using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="ToggleGroup.allowSwitchOff"/>.
    /// </summary>
    /// <remarks>
    /// Turning it off does not select anything: Unity leaves an already-empty group empty, and the first
    /// toggle the user presses becomes the selection.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(ToggleGroup), serializePropertyNames: "m_AllowSwitchOff")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/ToggleGroup Binder – Allow Switch Off")]
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
