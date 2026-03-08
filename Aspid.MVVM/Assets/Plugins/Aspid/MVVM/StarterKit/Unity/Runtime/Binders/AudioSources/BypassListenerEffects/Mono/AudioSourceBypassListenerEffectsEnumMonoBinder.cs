using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.bypassListenerEffects"/> property on an <see cref="AudioSource"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassListenerEffects Enum")]
    public sealed class AudioSourceBypassListenerEffectsEnumMonoBinder : EnumMonoBinder<AudioSource, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.bypassListenerEffects = value;
    }
}