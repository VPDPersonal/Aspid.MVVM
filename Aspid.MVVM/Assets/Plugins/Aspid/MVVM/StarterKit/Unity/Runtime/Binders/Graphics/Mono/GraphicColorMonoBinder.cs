using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color")]
    public class GraphicColorMonoBinder : ComponentColorMonoBinder<Graphic>
    {
        protected sealed override Color Property
        {
            get => CachedComponent.color;
            set => CachedComponent.color = value;
        }
    }
}