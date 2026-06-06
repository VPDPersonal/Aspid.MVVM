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
    /// <see cref="EnumGroupMonoBinder{Graphic, Material, Converter}"/> that sets the <see cref="Graphic.material"/>
    /// property on each <see cref="Graphic"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material", SubPath = "EnumGroup")]
    public sealed class GraphicMaterialEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic, Material, Converter>
    {
        /// <inheritdoc/>
        protected override void SetValue(Graphic element, Material value) =>
            element.material = value;
    }
}