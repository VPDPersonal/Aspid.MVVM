using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{T1, T2}"/> that sets the <see cref="Selectable.colors"/>
    /// property on each <see cref="Selectable"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock EnumGroup")]
    public sealed class SelectableColorBlockEnumGroupMonoBinder : EnumGroupMonoBinder<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        protected override void SetValue(Selectable element, ColorBlock value) =>
            element.colors = value;
    }
}