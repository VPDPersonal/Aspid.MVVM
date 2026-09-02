using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2}"/> that sets the <see cref="Selectable.colors"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock Enum")]
    public sealed class SelectableColorBlockEnumMonoBinder : EnumMonoBinder<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        protected override void SetValue(ColorBlock value) =>
            CachedComponent.colors = value;
    }
}