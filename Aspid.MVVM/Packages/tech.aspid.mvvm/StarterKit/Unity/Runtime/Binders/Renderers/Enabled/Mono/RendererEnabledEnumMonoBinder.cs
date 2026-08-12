using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{Renderer, Boolean}"/> that sets <see cref="Renderer.enabled"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Enabled", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Enabled Enum")]
    public sealed class RendererEnabledEnumMonoBinder : EnumMonoBinder<Renderer, bool>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}
