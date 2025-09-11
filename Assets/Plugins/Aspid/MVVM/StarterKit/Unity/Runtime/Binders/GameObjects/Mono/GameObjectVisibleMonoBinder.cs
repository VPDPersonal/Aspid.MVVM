using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Visible")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/GameObject/GameObject Binder - Visible")]
    public partial class GameObjectVisibleMonoBinder : MonoBinder, IBinder<bool>
    {
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            gameObject.SetActive(_isInvert ? !value : value);
    }
}