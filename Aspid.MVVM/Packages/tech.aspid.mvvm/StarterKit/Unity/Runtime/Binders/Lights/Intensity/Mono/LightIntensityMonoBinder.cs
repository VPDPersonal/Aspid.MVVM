using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Light}"/> that binds <see cref="Light.intensity"/>.
    /// </summary>
    /// <remarks>Non-finite values are dropped instead of being written.</remarks>
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
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.intensity = value;
            }
        }
    }
}
