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
    /// MonoBehaviour binder that sets a specific color channel (R, G, B, or A) on a <see cref="UnityEngine.UI.Graphic"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component Enum")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Enum")]
    public sealed class GraphicColorComponentEnumMonoBinder : EnumMonoBinder<Graphic, float, Converter>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;
        
        protected override void SetValue(float value) =>
            CachedComponent.SetColorComponent(_colorComponent, value);
    }
}