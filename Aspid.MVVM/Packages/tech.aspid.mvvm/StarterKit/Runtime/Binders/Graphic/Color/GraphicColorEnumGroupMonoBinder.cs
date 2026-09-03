using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Graphic.color"/> per group element.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "EnumGroup")]
    public sealed class GraphicColorEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic, Color>
    {
        /// <inheritdoc/>
        protected override void SetValue(Graphic element, Color value) =>
            element.color = value;
    }
}