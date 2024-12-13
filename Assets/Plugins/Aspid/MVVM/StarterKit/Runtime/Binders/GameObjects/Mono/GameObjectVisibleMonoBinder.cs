using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/GameObject/GameObject Binder - Visible")]
    public sealed partial class GameObjectVisibleMonoBinder : MonoBinder, IBinder<bool>
    {
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            gameObject.SetActive(_isInvert ? !value : value);
    }
}