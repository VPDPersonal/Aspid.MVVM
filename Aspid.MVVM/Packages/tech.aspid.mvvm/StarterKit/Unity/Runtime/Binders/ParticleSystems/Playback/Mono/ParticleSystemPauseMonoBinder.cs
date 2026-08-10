using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ParticleSystemPlaybackMonoBinder"/> that calls <see cref="ParticleSystem.Pause"/> when the bound
    /// ViewModel command or action is invoked.
    /// </summary>
    /// <remarks>
    /// Freezes the system where it is, children included. The particles stay on screen and resume from the same
    /// place when <see cref="ParticleSystem.Play"/> is called again.
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
