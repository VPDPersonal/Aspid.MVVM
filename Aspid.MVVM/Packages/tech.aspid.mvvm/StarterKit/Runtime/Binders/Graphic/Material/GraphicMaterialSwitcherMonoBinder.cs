using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="Graphic.material"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material Switcher")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material", SubPath = "Switcher")]
    public sealed class GraphicMaterialSwitcherMonoBinder : SwitcherMonoBinder<Graphic, Material>
    {
        /// <inheritdoc/>
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}