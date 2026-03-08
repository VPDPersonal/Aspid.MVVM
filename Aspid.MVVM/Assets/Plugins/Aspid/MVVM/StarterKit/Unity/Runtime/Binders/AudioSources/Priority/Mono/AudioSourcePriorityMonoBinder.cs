using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.priority"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes. The value is clamped to the range [0, 256].
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority")]
    public class AudioSourcePriorityMonoBinder : ComponentIntMonoBinder<AudioSource>
    {
        protected sealed override int Property
        {
            get => CachedComponent.priority;
            set => CachedComponent.priority = value;
        }

        protected override int GetConvertedValue(int value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 256);
    }
}