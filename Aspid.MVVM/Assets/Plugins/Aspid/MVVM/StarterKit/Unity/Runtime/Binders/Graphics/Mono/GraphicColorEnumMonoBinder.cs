using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumColorMonoBinder{Graphic}"/> that sets the <see cref="Graphic.color"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Enum")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Enum")]
    public sealed class GraphicColorEnumMonoBinder : EnumColorMonoBinder<Graphic>
    {
        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            CachedComponent.color = value;
    }
}