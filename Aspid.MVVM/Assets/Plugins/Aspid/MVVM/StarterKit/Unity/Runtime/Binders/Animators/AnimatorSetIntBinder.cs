#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class AnimatorSetIntBinder : AnimatorSetParameterBinder<int>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public AnimatorSetIntBinder(Animator animator, string parameterName, BindMode mode)
            : this(animator, parameterName, converter: null, mode) { }
        
        public AnimatorSetIntBinder(
            Animator animator,
            string parameterName, 
            Converter? converter = null, 
            BindMode mode = BindMode.OneWay)
            : base(animator, parameterName, mode)
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