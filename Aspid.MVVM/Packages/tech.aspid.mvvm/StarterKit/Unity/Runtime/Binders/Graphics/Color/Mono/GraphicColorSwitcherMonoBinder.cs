using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherColorMonoBinder{Graphic}"/> that switches the <see cref="Graphic.color"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Switcher")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Switcher")]
    public sealed class GraphicColorSwitcherMonoBinder : SwitcherColorMonoBinder<Graphic>
    {
        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            CachedComponent.color = value;
    }
}