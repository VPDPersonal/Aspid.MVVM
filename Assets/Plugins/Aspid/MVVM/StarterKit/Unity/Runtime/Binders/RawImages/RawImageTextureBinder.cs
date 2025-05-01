#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class RawImageTextureBinder : TargetBinder<RawImage>, IBinder<Texture2D?>, IBinder<Sprite?>
    {
        [Header("Parameter")]
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
            if (_disabledWhenNull) Target.enabled = value is not null;
        }

        public void SetValue(Sprite? value) =>
            SetValue(value?.texture);
    }
}