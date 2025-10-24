#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class RawImageTextureBinder : TargetBinder<RawImage>, IBinder<Texture2D?>, IBinder<Sprite?>
    {
        [SerializeField] private bool _disabledWhenNull;

        public RawImageTextureBinder(RawImage target, BindMode mode)
            : this(target, true, mode) { }
        
        public RawImageTextureBinder(RawImage target, bool disabledWhenNull = true, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _disabledWhenNull = disabledWhenNull;
        }

        public void SetValue(Texture2D? value)
        {
            Target.texture = value;
            Target.enabled = !_disabledWhenNull || value;
        }

        public void SetValue(Sprite? value) =>
            SetValue(value?.texture);
    }
}