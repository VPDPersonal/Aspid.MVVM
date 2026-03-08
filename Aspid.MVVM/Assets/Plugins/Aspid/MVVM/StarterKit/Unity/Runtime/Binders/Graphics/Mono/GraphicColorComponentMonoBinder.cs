using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets a specific color channel (R, G, B, or A) on a <see cref="UnityEngine.UI.Graphic"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component")]
    public class GraphicColorComponentMonoBinder : ComponentFloatMonoBinder<Graphic>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;
        
        protected sealed override float Property
        {
            get => CachedComponent.GetColorComponent(_colorComponent);
            set => CachedComponent.SetColorComponent(_colorComponent, GetConvertedValue(value));
        }
    }
}