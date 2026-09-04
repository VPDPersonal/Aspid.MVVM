using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ParticleSystemPlaybackMonoBinder"/> that calls <see cref="ParticleSystem.Pause()"/>.
    /// </summary>
    /// <remarks>
    /// Particles stay in place until <see cref="ParticleSystem.Play()"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(ParticleSystem), SubPath = "Playback")]
    [AddComponentMenu("Aspid/MVVM/Binders/Effects/ParticleSystem/ParticleSystem Binder – Pause")]
    public sealed class ParticleSystemPauseMonoBinder : ParticleSystemPlaybackMonoBinder
    {
        /// <inheritdoc/>
        protected override void Perform(ParticleSystem particleSystem) =>
            particleSystem.Pause();
    }
}
