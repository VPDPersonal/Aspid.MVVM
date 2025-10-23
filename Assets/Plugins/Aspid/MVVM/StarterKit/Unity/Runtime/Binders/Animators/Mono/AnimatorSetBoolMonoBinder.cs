using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder - Set Bool")]
    [AddComponentContextMenu(typeof(Animator),"Add Animator Binder/Animator Binder - Set Bool")]
    public class AnimatorSetBoolMonoBinder : AnimatorSetParameterMonoBinder<bool>
    {
        [SerializeField] private bool _isInvert;
        
        protected sealed override void SetParameter(bool value)
        { 
            value = _isInvert ? !value : value;
            if (value == CachedComponent.GetBool(ParameterName)) return;
            
            CachedComponent.SetBool(ParameterName, value);
        }
    }
}