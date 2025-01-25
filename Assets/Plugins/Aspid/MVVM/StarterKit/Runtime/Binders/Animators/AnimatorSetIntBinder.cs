#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterInt;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetIntBinder : AnimatorSetParameterBinder<int>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public AnimatorSetIntBinder(
            Animator animator,
            string parameterName, 
            Func<int, int> converter)
            : this(animator, parameterName, converter.ToConvert()) { }
        
        public AnimatorSetIntBinder(
            Animator animator,
            string parameterName, 
            Converter? converter = null)
            : base(animator, parameterName)
        {
            _converter = converter;
        }

        protected sealed override void SetParameter(int value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, Target.GetInteger(ParameterName))) return;
            
            Target.SetInteger(ParameterName, value);
        }
    }
}