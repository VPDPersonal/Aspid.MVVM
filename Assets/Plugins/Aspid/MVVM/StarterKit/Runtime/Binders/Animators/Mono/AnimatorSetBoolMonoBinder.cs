using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Animator/Animator Binder - Set Bool")]
    public class AnimatorSetBoolMonoBinder : AnimatorSetParameterMonoBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private string _parameterName;
        [SerializeField] private bool _isInvert;
        
        protected string ParameterName => _parameterName;
        
        protected sealed override void SetParameter(bool value) =>
            CachedComponent.SetBool(ParameterName, _isInvert ? !value : value);

        protected override bool CanExecute(bool value) =>
            base.CanExecute(value) && CachedComponent.GetBool(ParameterName) != value;
    }
}