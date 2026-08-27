using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinderWithConverter{T1, T2}"/> that switches the <see cref="Selectable.colors"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock Switcher")]
    public sealed class SelectableColorBlockSwitcherMonoBinder : SwitcherMonoBinderWithConverter<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        protected override void SetValue(ColorBlock value) =>
            CachedComponent.colors = value;
    }
}