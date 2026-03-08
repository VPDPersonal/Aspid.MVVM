using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.bypassEffects"/> property on a group of <see cref="AudioSource"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassEffects EnumGroup")]
    public sealed class AudioSourceBypassEffectsEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, bool>
    {
        protected override void SetValue(AudioSource element, bool value) =>
            element.bypassEffects = value;
    }
}