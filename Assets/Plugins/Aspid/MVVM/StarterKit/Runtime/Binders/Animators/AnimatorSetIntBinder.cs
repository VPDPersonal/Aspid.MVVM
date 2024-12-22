#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetIntBinder : AnimatorSetParameterBinder<int>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<int, int>? _converter;

        public AnimatorSetIntBinder(Animator animator, string parameterName, IConverter<int, int>? converter = null)
            : base(animator, parameterName)
        {
            _converter = converter;
        }

        protected sealed override void SetParameter(int value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, Animator.GetInteger(ParameterName))) return;
            
            Animator.SetInteger(ParameterName, value);
        }
    }
}