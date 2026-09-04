using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Selectable.colors"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Colors", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Color Block Enum")]
    public sealed class SelectableColorBlockEnumMonoBinder : EnumMonoBinder<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        protected override void SetValue(ColorBlock value) =>
            CachedComponent.colors = value;
    }
}
