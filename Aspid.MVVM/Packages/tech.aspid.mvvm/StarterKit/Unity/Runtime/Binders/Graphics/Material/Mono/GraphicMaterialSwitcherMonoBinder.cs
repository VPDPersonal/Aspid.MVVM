using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{T1, T2}"/> that switches the <see cref="Graphic.material"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material Switcher")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material", SubPath = "Switcher")]
    public sealed class GraphicMaterialSwitcherMonoBinder : SwitcherMonoBinder<Graphic, Material>
    {
        /// <inheritdoc/>
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}