#nullable enable
using System;
using UnityEngine;
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
    public class GraphicColorComponentBinder : TargetBinder<Graphic>, INumberBinder
    {
        [SerializeField] private ColorComponent _component = ColorComponent.A;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public GraphicColorComponentBinder(Graphic target, ColorComponent component, BindMode mode)
            : this(target, component, converter: null,  mode) { }
        
        public GraphicColorComponentBinder(Graphic target, ColorComponent component = ColorComponent.A, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _component = component;
            _converter = converter;
        }
        
        public void SetValue(int value) =>
            SetValue((float)value);

        public void SetValue(long value) =>
            SetValue((float)value);

        public void SetValue(float value) =>
            Target.SetColor(_component, _converter?.Convert(value) ?? value);

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}