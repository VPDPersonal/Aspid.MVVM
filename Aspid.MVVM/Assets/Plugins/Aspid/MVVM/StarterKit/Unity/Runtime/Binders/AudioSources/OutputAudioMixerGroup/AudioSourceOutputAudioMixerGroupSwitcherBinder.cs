#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{AudioSource, AudioMixerGroup}"/> that switches the <see cref="AudioSource.outputAudioMixerGroup"/>
    /// property between two <see cref="AudioMixerGroup"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the AudioSource outputAudioMixerGroup between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceOutputAudioMixerGroupSwitcherBinder _isMixerActive;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isMixerActive;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///     [SerializeField] private AudioMixerGroup _activeMixer;
    ///    
    ///     private AudioSourceOutputAudioMixerGroupSwitcherBinder IsMixerActive => new(
    ///         _audioSource, _activeMixer, null);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isMixerActive;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class AudioSourceOutputAudioMixerGroupSwitcherBinder : SwitcherBinder<AudioSource, AudioMixerGroup?>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceOutputAudioMixerGroupSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.outputAudioMixerGroup"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="AudioMixerGroup"/> assigned when the bound value is <see langword="true"/>, or <see langword="null"/> to clear the output group.</param>
        /// <param name="falseValue">The <see cref="AudioMixerGroup"/> assigned when the bound value is <see langword="false"/>, or <see langword="null"/> to clear the output group.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceOutputAudioMixerGroupSwitcherBinder(
            AudioSource target,
            AudioMixerGroup? trueValue,
            AudioMixerGroup? falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(AudioMixerGroup? value) =>
            Target.outputAudioMixerGroup = value;
    }
}