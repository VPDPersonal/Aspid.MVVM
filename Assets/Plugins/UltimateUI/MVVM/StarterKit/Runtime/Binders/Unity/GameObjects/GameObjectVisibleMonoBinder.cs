using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.GameObjects
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