using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{   
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