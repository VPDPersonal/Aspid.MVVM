#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{AudioSource, AudioClip}"/> that sets the <see cref="AudioSource.clip"/> property.
    /// </summary>
    /// <example>
    /// Set the AudioSource clip to a ViewModel AudioClip value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceClipBinder _clip;
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public AudioClip _clip;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///     
    ///     private AudioSourceClipBinder Clip =>
    ///         new(_audioSource);
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public AudioClip _clip;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class AudioSourceClipBinder : TargetBinder<AudioSource, AudioClip>
    {
        /// <inheritdoc/>
        protected sealed override AudioClip? Property
        {
            get => Target.clip;
            set => Target.clip = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceClipBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.clip"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceClipBinder(AudioSource target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}