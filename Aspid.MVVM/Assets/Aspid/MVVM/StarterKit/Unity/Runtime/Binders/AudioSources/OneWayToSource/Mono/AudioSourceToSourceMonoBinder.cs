using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{AudioSource}"/> that sends the cached <see cref="AudioSource"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource To Source Binder")]
    public sealed class AudioSourceToSourceMonoBinder : ComponentToSourceMonoBinder<AudioSource> { }
}