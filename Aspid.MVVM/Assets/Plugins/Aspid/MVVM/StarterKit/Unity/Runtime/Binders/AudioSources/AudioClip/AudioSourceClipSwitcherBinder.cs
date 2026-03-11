#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{AudioSource, AudioClip}"/> that switches the <see cref="AudioSource.clip"/>
    /// property between two <see cref="AudioClip"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the AudioSource clip between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceClipSwitcherBinder _isPlaying;
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isPlaying;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///     [SerializeField] private AudioClip _playingClip;
    ///     [SerializeField] private AudioClip _pausedClip;
    ///     
    ///     private AudioSourceClipSwitcherBinder IsPlaying => new(
    ///         _audioSource, _playingClip, _pausedClip);
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isPlaying;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class AudioSourceClipSwitcherBinder : SwitcherBinder<AudioSource, AudioClip?>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceClipSwitcherBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.clip"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="AudioClip"/> assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="AudioClip"/> assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public AudioSourceClipSwitcherBinder(
            AudioSource target,
            AudioClip? trueValue,
            AudioClip? falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(AudioClip? value) =>
            Target.clip = value;
    }
}