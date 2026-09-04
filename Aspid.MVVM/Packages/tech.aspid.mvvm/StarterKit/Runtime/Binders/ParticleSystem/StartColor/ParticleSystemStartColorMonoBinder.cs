using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds
    /// <see cref="ParticleSystem.MainModule.startColor"/>.
    /// </summary>
    /// <remarks>
    /// Only particles emitted after the write take the color; a gradient or range collapses to a single value.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(ParticleSystem), serializePropertyNames: "startColor")]
    [AddComponentMenu("Aspid/MVVM/Binders/Effects/ParticleSystem/ParticleSystem Binder – Start Color")]
    public class ParticleSystemStartColorMonoBinder : ComponentMonoBinder<ParticleSystem, Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.main.startColor.color;
            set
            {
                var main = CachedComponent.main;
                main.startColor = value;
            }
        }
    }
}
