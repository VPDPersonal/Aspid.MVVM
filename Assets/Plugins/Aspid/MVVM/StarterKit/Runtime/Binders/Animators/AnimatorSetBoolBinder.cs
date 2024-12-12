using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetBoolBinder : AnimatorSetParameterBinder<bool>
    {
        [field: SerializeField]
        protected string ParameterName { get; private set; }

        public AnimatorSetBoolBinder(Animator animator, string parameterName) : base(animator)
        {
            ParameterName = parameterName;
        }

        protected sealed override void SetParameter(bool value) =>
            Animator.SetBool(ParameterName, value);
        
        protected override bool CanExecute(bool value) =>
            base.CanExecute(value) && Animator.GetBool(ParameterName) != value;
    }
}