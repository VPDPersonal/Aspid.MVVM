using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Graphic, Color}"/> that binds the <see cref="Graphic.color"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color")]
    public class GraphicColorMonoBinder : ComponentMonoBinder<Graphic, Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.color;
            set => CachedComponent.color = value;
        }
    }
}