#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{AudioSource, AudioMixerGroup}"/> that sets the <see cref="AudioSource.outputAudioMixerGroup"/> property.
    /// </summary>
    /// <example>
    /// Set the AudioSource outputAudioMixerGroup based on a ViewModel AudioMixerGroup value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceOutputAudioMixerGroupBinder _outputAudioMixerGroup;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public AudioMixerGroup _outputAudioMixerGroup;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///    
    ///     private AudioSourceOutputAudioMixerGroupBinder OutputAudioMixerGroup =>
    ///         new(_audioSource);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public AudioMixerGroup _outputAudioMixerGroup;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class AudioSourceOutputAudioMixerGroupBinder : TargetBinder<AudioSource, AudioMixerGroup>
    {
        /// <inheritdoc/>
        protected sealed override AudioMixerGroup? Property
        {
            get => Target.outputAudioMixerGroup;
            set => Target.outputAudioMixerGroup = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceOutputAudioMixerGroupBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.outputAudioMixerGroup"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceOutputAudioMixerGroupBinder(AudioSource target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}