using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetIntBinder : AnimatorSetParameterBinder<int>
    {
        [field: SerializeField]
        protected string ParameterName { get; private set; }

        public AnimatorSetIntBinder(Animator animator, string parameterName) : base(animator)
        {
            ParameterName = parameterName;
        }

        protected sealed override void SetParameter(int value) =>
            Animator.SetFloat(ParameterName, value);
        
        protected override bool CanExecute(int value) =>
            base.CanExecute(value) && Animator.GetInteger(ParameterName) != value;
    }
}