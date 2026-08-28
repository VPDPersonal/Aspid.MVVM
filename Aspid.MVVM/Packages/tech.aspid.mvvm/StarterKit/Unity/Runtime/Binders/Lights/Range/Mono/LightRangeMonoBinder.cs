using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Light}"/> that binds <see cref="Light.range"/>.
    /// </summary>
    /// <remarks>
    /// Non-finite values are dropped instead of the zero Unity would otherwise coerce them to, which would
    /// switch the light off.
    /// </remarks>
    [AddBinderContextMenu(typeof(Light), serializePropertyNames: "m_Range")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Light Binder – Range")]
    public class LightRangeMonoBinder : ComponentFloatMonoBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.range;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.range = value;
            }
        }
    }
}
