#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetBoolBinder : AnimatorSetParameterBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public AnimatorSetBoolBinder(Animator animator, string parameterName, BindMode mode)
            : this(animator, parameterName, false, mode) { }
        
        public AnimatorSetBoolBinder(
            Animator animator, 
            string parameterName, 
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(animator, parameterName, mode)
        {
            _isInvert = isInvert;
        }

        protected sealed override void SetParameter(bool value)
        { 
            value = _isInvert ? !value : value;
            if (value == Target.GetBool(ParameterName)) return;
            
            Target.SetBool(ParameterName, value);
        }
    }
}