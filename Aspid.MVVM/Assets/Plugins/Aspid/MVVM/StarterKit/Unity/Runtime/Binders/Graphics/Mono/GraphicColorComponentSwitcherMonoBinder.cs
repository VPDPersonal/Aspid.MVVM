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
    /// MonoBehaviour binder that switches a specific color channel (R, G, B, or A) on a <see cref="UnityEngine.UI.Graphic"/>
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component Switcher")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Switcher")]
    public sealed class GraphicColorComponentSwitcherMonoBinder : SwitcherMonoBinder<Graphic, float, Converter>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;

        protected override void SetValue(float value) =>
            CachedComponent.SetColorComponent(_colorComponent, value);
    }
}