using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{T1, T2}">EnumGroupMonoBinder&lt;SpriteRenderer, bool&gt;</see> that sets <see cref="SpriteRenderer.flipX"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_FlipX", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Flip X EnumGroup")]
    public sealed class SpriteRendererFlipXEnumGroupMonoBinder : EnumGroupMonoBinder<SpriteRenderer, bool>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// </summary>
        protected override void SetValue(SpriteRenderer element, bool value) =>
            element.flipX = value;
    }
}
