using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Light}"/> that binds <see cref="Light.spotAngle"/>.
    /// </summary>
    /// <remarks>
    /// Unity clamps the angle to 1–179 degrees on its own; non-finite values are dropped instead of being written.
    /// </remarks>
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
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.spotAngle = value;
            }
        }
    }
}
