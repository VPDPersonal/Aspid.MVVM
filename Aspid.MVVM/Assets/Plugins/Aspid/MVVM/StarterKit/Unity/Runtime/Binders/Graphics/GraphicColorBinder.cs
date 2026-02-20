#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class GraphicColorBinder : TargetColorBinder<Graphic>
    {
        protected sealed override Color Property
        {
            get => Target.color;
            set => Target.color = value;
        }
        
        public GraphicColorBinder(Graphic target, BindMode mode)
            : this(target, converter: null,  mode) { }
        
        public GraphicColorBinder(Graphic target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}