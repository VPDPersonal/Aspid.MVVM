#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class AnimatorSetFloatBinder : AnimatorSetParameterBinder<float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public AnimatorSetFloatBinder(Animator animator, string parameterName, BindMode mode)
            : this(animator, parameterName, null, mode) { }
        
        public AnimatorSetFloatBinder(
            Animator animator,
            string parameterName, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(animator, parameterName, mode)
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