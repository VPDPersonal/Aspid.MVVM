using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource To Source Binder")]
    public sealed class AudioSourceToSourceMonoBinder : ComponentToSourceMonoBinder<AudioSource> { }
}