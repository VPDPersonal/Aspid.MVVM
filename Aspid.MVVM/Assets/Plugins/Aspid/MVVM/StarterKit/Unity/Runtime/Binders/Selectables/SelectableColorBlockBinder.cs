#nullable enable
using System;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.UI.ColorBlock, UnityEngine.UI.ColorBlock>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColorBlock;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class SelectableColorBlockBinder : TargetBinder<Selectable, ColorBlock, Converter>
    {
        protected sealed override ColorBlock Property
        {
            get => Target.colors;
            set => Target.colors = value;
        }
        
        public SelectableColorBlockBinder(Selectable target, BindMode mode) 
            : this(target, converter: null, mode) { }

        public SelectableColorBlockBinder(Selectable target, Converter? converter = null, BindMode mode = BindMode.OneWay) 
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}