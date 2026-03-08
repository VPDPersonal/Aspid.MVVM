#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that switches the <see cref="AudioSource.priority"/> between two integer values based
    /// on a bound boolean ViewModel property. The applied value is clamped to [0, 256].
    /// </summary>
    [Serializable]
    public sealed class AudioSourcePrioritySwitcherBinder : SwitcherBinder<AudioSource, int, Converter>
    {
        public AudioSourcePrioritySwitcherBinder(
            AudioSource target,
            int trueValue,
            int falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public AudioSourcePrioritySwitcherBinder(
            AudioSource target,
            int trueValue,
            int falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }
        
        protected override void SetValue(int value) =>
            Target.priority = Mathf.Clamp(value, min: 0, max: 256);
    }
}