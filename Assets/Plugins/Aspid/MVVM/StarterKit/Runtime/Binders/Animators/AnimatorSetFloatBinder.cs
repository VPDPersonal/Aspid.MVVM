#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetFloatBinder : AnimatorSetParameterBinder<float>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;

        public AnimatorSetFloatBinder(
            Animator animator,
            string parameterName, 
            IConverter<float, float>? converter = null)
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