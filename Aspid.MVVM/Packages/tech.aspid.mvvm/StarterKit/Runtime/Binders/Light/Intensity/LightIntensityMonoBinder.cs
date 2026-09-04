using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Light.intensity"/>.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Light), serializePropertyNames: "m_Intensity")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Light Binder – Intensity")]
    public class LightIntensityMonoBinder : ComponentFloatMonoBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.intensity;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.intensity = value;
            }
        }
    }
}
