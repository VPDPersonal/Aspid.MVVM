using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentColorMonoBinder{ParticleSystem}"/> that binds
    /// <see cref="ParticleSystem.MainModule.startColor"/>.
    /// </summary>
    /// <remarks>
    /// Only particles emitted after the write take the new color; particles already alive keep theirs. Writing a
    /// <see cref="Color"/> replaces whatever the module held, collapsing a gradient- or range-authored start color
    /// to a single value.
    /// </remarks>
    [AddBinderContextMenu(typeof(ParticleSystem), serializePropertyNames: "startColor")]
    [AddComponentMenu("Aspid/MVVM/Binders/Effects/ParticleSystem/ParticleSystem Binder – Start Color")]
    public class ParticleSystemStartColorMonoBinder : ComponentColorMonoBinder<ParticleSystem>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.main.startColor.color;
            set
            {
                // main is a struct wrapper; write through a local copy since the property can't be accessed by ref.
                var main = CachedComponent.main;
                main.startColor = value;
            }
        }
    }
}
