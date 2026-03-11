#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{AudioSource}"/> that sets the <see cref="AudioSource.bypassEffects"/> property.
    /// </summary>
    /// <example>
    /// Set the AudioSource bypassEffects property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private AudioSourceBypassEffectsBinder _bypassEffects;
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _bypassEffects;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private AudioSource _audioSource;
    ///
    ///     private AudioSourceBypassEffectsBinder BypassEffects =>
    ///         new(_audioSource);
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _bypassEffects;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class AudioSourceBypassEffectsBinder : TargetBoolBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.bypassEffects;
            set => Target.bypassEffects = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceBypassEffectsBinder"/> targeting the specified <see cref="AudioSource"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.bypassEffects"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceBypassEffectsBinder(AudioSource target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AudioSourceBypassEffectsBinder"/> targeting the specified <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="target">The <see cref="AudioSource"/> whose <see cref="AudioSource.bypassEffects"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AudioSourceBypassEffectsBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}