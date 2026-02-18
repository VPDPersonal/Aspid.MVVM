#nullable enable
using System;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class HorizontalOrVerticalLayoutSpacingBinder : TargetFloatBinder<HorizontalOrVerticalLayoutGroup>, INumberBinder
    {
        protected sealed override float Property
        {
            get => Target.spacing;
            set => Target.spacing = value;
        }
        
        public HorizontalOrVerticalLayoutSpacingBinder(
            HorizontalOrVerticalLayoutGroup target, 
            BindMode bindMode = BindMode.OneWay)
            : this(target, converter: null, bindMode) { }
        
        public HorizontalOrVerticalLayoutSpacingBinder(
            HorizontalOrVerticalLayoutGroup target, 
            Converter? converter = null,
            BindMode bindMode = BindMode.OneWay)
            : base(target, converter, bindMode)
        {
            bindMode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}

