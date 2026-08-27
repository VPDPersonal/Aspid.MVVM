using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{ToggleGroup}"/> that binds <see cref="ToggleGroup.allowSwitchOff"/>.
    /// </summary>
    /// <remarks>
    /// Whether the group may end up with nothing selected — the difference between a filter the player can clear and
    /// a set of tabs that must always have one open.
    /// <para/>
    /// Turning it off does not select anything: Unity leaves an already-empty group empty, and the first toggle the user
    /// presses becomes the selection. A ViewModel that needs one selected from the start has to say which.
    /// </remarks>
    [AddBinderContextMenu(typeof(ToggleGroup), serializePropertyNames: "m_AllowSwitchOff")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/ToggleGroup Binder – Allow Switch Off")]
    public class ToggleGroupAllowSwitchOffMonoBinder : ComponentBoolMonoBinder<ToggleGroup>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.allowSwitchOff;
            set => CachedComponent.allowSwitchOff = value;
        }
    }
}
