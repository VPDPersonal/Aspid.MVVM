using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.GameObjects
{
    public partial class GameObjectVisibleBinder : MonoBinder, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private bool _isInvert;
        
        protected bool IsInvert => _isInvert;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            if (IsInvert) value = !value;
            gameObject.SetActive(value);
        }
    }
}