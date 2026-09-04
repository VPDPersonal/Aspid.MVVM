using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Selectable.colors"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Colors", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Color Block EnumGroup")]
    public sealed class SelectableColorBlockEnumGroupMonoBinder : EnumGroupMonoBinder<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        protected override void SetValue(Selectable element, ColorBlock value) =>
            element.colors = value;
    }
}
