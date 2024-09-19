using UnityEngine;
using AspidUI.MVVM.Unity.Generation;
using MonoBinder = AspidUI.MVVM.Unity.MonoBinder;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.GameObjects
{
    [AddComponentMenu("UI/Binders/GameObject/GameObject Binder - Visible")]
    public partial class GameObjectVisibleMonoBinder : MonoBinder, IBinder<bool>
    {
        [field: Header("Parameter")]
        [field: SerializeReference]
        protected bool IsInvert { get; private set; }
        
        [BinderLog]
        public void SetValue(bool value)
        {
            if (IsInvert) value = !value;
            gameObject.SetActive(value);
        }
    }
}