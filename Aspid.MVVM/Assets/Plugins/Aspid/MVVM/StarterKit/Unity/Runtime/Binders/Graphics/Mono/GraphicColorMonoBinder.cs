using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentColorMonoBinder{Graphic}"/> that binds the <see cref="Graphic.color"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current color value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color")]
    public class GraphicColorMonoBinder : ComponentColorMonoBinder<Graphic>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.color;
            set => CachedComponent.color = value;
        }
    }
}