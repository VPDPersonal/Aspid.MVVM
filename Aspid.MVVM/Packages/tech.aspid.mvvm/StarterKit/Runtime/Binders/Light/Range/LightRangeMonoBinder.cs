using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Light.range"/>.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.range = value;
            }
        }
    }
}
