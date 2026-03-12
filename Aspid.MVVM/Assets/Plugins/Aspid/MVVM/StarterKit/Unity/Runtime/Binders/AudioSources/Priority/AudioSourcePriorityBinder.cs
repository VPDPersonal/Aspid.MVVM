#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{AudioSource}"/> that sets the <see cref="AudioSource.priority"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 256] before being applied to <see cref="AudioSource.priority"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-Priority-1.1.0.xml" path="doc//member[@name='AudioSourcePriorityBinder']/*" />
    [Serializable]
    public class AudioSourcePriorityBinder : TargetIntBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.priority;
            set => Target.priority = value;
        }
        
        /// <inheritdoc />
        public AudioSourcePriorityBinder(AudioSource target, IConverter<int, int>? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.priority"/> property.
        /// Clamps the converted value to the valid range of 0 to 256.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override int GetConvertedValue(int value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 256);
    }
}