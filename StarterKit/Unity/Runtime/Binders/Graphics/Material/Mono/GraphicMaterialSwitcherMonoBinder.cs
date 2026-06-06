using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{Graphic, Material, Converter}"/> that switches the <see cref="Graphic.material"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material Switcher")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material", SubPath = "Switcher")]
    public sealed class GraphicMaterialSwitcherMonoBinder : SwitcherMonoBinder<Graphic, Material, Converter>
    {
        /// <inheritdoc/>
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}