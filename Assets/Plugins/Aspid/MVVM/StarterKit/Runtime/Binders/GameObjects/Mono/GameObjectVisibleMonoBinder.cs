using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/GameObject/GameObject Binder - Visible")]
    public partial class GameObjectVisibleMonoBinder : MonoBinder, IBinder<bool>
    {
        [Header("Parameter")]
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            if (_isInvert) value = !value;
            gameObject.SetActive(value);
        }
    }

}