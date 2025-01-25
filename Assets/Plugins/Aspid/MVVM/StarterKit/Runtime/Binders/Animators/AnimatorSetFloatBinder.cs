#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetFloatBinder : AnimatorSetParameterBinder<float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public AnimatorSetFloatBinder(
            Animator animator,
            string parameterName, 
            Func<float, float> converter)
            : this(animator, parameterName, converter.ToConvert()) { }
        
        public AnimatorSetFloatBinder(
            Animator animator,
            string parameterName, 
            Converter? converter = null)
            : base(animator, parameterName)
        {
            _converter = converter;
        }

        protected sealed override void SetParameter(float value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, Target.GetFloat(ParameterName))) return;
            
            Target.SetFloat(ParameterName, value);
        }
    }
}