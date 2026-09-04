using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Light.spotAngle"/>.
    /// </summary>
    /// <remarks>
    /// Unity clamps the angle to [1, 179]; a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Light), serializePropertyNames: "m_SpotAngle")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Light Binder – Spot Angle")]
    public class LightSpotAngleMonoBinder : ComponentFloatMonoBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.spotAngle;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.spotAngle = value;
            }
        }
    }
}
