using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.GameObjects
{
    [AddComponentMenu("UI/Binders/GameObject/GameObject Binder - Visible")]
    public partial class GameObjectVisibleMonoBinder : Aspid.UI.MVVM.Mono.MonoBinder, IBinder<bool>
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