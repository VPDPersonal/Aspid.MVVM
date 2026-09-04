using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="Selectable.colors"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Colors", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Color Block Switcher")]
    public sealed class SelectableColorBlockSwitcherMonoBinder : SwitcherMonoBinder<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        protected override void SetValue(ColorBlock value) =>
            CachedComponent.colors = value;
    }
}
