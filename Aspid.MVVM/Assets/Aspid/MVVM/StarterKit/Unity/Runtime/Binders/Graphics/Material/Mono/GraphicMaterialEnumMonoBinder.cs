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
    /// <see cref="EnumMonoBinder{Graphic, Material, Converter}"/> that sets the <see cref="Graphic.material"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material Enum")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material", SubPath = "Enum")]
    public sealed class GraphicMaterialEnumMonoBinder : EnumMonoBinder<Graphic, Material, Converter>
    {
        /// <inheritdoc/>
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}