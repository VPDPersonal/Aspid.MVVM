using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ParticleSystemPlaybackMonoBinder"/> that calls <see cref="ParticleSystem.Clear()"/>.
    /// </summary>
    /// <remarks>
    /// Removes every alive particle without changing the playback state.
    /// </remarks>
    [AddBinderContextMenu(typeof(ParticleSystem), SubPath = "Playback")]
    [AddComponentMenu("Aspid/MVVM/Binders/Effects/ParticleSystem/ParticleSystem Binder – Clear")]
    public sealed class ParticleSystemClearMonoBinder : ParticleSystemPlaybackMonoBinder
    {
        /// <inheritdoc/>
        protected override void Perform(ParticleSystem particleSystem) =>
            particleSystem.Clear();
    }
}
