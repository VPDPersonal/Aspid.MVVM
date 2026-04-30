using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupColorMonoBinder{Graphic}"/> that sets the <see cref="Graphic.color"/>
    /// property on each <see cref="Graphic"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "EnumGroup")]
    public sealed class GraphicColorEnumGroupMonoBinder : EnumGroupColorMonoBinder<Graphic>
    {
        /// <inheritdoc/>
        protected override void SetValue(Graphic element, Color value) =>
            element.color = value;
    }
}