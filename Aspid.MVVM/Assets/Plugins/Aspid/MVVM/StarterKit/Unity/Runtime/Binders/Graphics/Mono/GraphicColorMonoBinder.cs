using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Graphic.color"/> property on a <see cref="Graphic"/>
    /// when the bound ViewModel value changes. Supports <see cref="BindMode.OneWayToSource"/>: when binding
    /// is established the current <see cref="Graphic.color"/> value is sent back to the ViewModel.
    /// </summary>
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