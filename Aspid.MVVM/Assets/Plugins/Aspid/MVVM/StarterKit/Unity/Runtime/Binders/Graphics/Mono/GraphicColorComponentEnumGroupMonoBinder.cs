using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets a specific color channel (R, G, B, or A) on a group of <see cref="UnityEngine.UI.Graphic"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "EnumGroup")]
    public sealed class GraphicColorComponentEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic, float, Converter>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;
        
        protected override void SetValue(Graphic element, float value) =>
            element.SetColorComponent(_colorComponent, value);
    }
}