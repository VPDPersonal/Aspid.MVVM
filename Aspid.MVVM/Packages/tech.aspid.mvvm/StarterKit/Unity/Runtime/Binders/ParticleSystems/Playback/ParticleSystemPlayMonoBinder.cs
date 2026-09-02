using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ParticleSystemPlaybackMonoBinder"/> that calls <see cref="ParticleSystem.Play()"/> when the bound
    /// ViewModel command or action is invoked.
    /// </summary>
    /// <remarks>
    /// Starts emitting, children included. A system already playing carries on rather than restarting — to replay
    /// an effect from the beginning, stop and clear it first.
    /// </remarks>
    [AddBinderContextMenu(typeof(ParticleSystem), SubPath = "Playback")]
    [AddComponentMenu("Aspid/MVVM/Binders/Effects/ParticleSystem/ParticleSystem Binder – Play")]
    public sealed class ParticleSystemPlayMonoBinder : ParticleSystemPlaybackMonoBinder
    {
        /// <inheritdoc/>
        protected override void Perform(ParticleSystem particleSystem) =>
            particleSystem.Play();
    }
}
