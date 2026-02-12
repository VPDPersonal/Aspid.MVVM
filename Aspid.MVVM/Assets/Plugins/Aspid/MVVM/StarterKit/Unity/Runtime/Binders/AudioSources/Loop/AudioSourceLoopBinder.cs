#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class AudioSourceLoopBinder : TargetBinder<AudioSource>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool>? ValueChanged;
        
        [SerializeField] private bool _isInvert;
        
        public AudioSourceLoopBinder(AudioSource target, BindMode mode)
            : this(target, isInvert: false, mode) { }
        
        public AudioSourceLoopBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isInvert = isInvert;
        }

        public void SetValue(bool value) =>
            Target.loop = GetConvertedValue(value);
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(Target.loop));
        }
        
        private bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}