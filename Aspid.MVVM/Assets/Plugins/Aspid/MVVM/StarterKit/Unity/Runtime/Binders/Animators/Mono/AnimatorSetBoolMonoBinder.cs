using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets a boolean parameter on a Unity <see cref="Animator"/> when
    /// the bound ViewModel value changes.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Bool")]
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