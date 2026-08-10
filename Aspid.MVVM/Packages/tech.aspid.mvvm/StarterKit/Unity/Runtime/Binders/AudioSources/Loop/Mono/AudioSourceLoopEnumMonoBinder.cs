using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2}">EnumMonoBinder&lt;AudioSource, bool&gt;</see> that sets the <see cref="AudioSource.loop"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Loop Enum")]
    public sealed class AudioSourceLoopEnumMonoBinder : EnumMonoBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.loop = value;
    }
}