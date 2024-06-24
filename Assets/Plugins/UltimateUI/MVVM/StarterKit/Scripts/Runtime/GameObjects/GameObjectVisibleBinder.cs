using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.GameObjects
{
    public class GameObjectVisibleBinder : MonoBinder, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private bool _isInvert;
        
        protected bool IsInvert => _isInvert;
        
        public void SetValue(bool value)
        {
            if (IsInvert) value = !IsInvert;
            gameObject.SetActive(value);
        }
    }
}