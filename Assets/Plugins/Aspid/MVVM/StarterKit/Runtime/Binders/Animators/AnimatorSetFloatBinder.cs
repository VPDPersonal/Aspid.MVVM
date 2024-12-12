using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetFloatBinder : AnimatorSetParameterBinder<float>
    {
        [field: SerializeField]
        protected string ParameterName { get; private set; }

        public AnimatorSetFloatBinder(Animator animator, string parameterName) : base(animator)
        {
            ParameterName = parameterName;
        }

        protected sealed override void SetParameter(float value) =>
            Animator.SetFloat(ParameterName, value);
        
        protected override bool CanExecute(float value) =>
            base.CanExecute(value) && !Mathf.Approximately(Animator.GetFloat(ParameterName), value);
    }
}