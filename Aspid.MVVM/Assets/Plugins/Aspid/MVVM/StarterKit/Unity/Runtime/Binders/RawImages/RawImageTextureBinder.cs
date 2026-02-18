#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture?, UnityEngine.Texture?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterTexture;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class RawImageTextureBinder : TargetBinder<RawImage, Texture, Converter>, IBinder<Sprite?>
    {
        [SerializeField] private bool _disabledWhenNull;
        
        protected sealed override Texture? Property
        {
            get => Target.texture;
            set
            {
                Target.texture = value;
                Target.enabled = !_disabledWhenNull || value;
            }
        }

        public RawImageTextureBinder(RawImage target, BindMode mode)
            : this(target, disabledWhenNull: true, null, mode) { }
        
        public RawImageTextureBinder(RawImage target, bool disabledWhenNull = true, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _disabledWhenNull = disabledWhenNull;
        }

        public void SetValue(Sprite? value) =>
            SetValue(value?.texture);
    }
}