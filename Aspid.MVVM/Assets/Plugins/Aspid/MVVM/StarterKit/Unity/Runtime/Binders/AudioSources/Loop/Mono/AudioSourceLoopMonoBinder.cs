using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Loop")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public partial class AudioSourceLoopMonoBinder : ComponentMonoBinder<AudioSource>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        [SerializeField] private bool _isInvert;

        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.loop = GetConvertedValue(value);
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(CachedComponent.loop));
        }
        
        private bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}