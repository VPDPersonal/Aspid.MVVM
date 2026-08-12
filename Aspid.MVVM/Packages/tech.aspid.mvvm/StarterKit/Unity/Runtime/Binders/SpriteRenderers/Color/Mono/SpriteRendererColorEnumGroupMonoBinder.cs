using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{SpriteRenderer, Color}"/> that sets <see cref="SpriteRenderer.color"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Color", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Color EnumGroup")]
    public sealed class SpriteRendererColorEnumGroupMonoBinder : EnumGroupMonoBinder<SpriteRenderer, Color>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// </summary>
        protected override void SetValue(SpriteRenderer element, Color value) =>
            element.color = value;
    }
}
