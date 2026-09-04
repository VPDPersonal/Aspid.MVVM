using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ParticleSystemPlaybackMonoBinder"/> that calls <see cref="ParticleSystem.Play()"/>.
    /// </summary>
    /// <remarks>
    /// A playing system carries on rather than restarting.
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
