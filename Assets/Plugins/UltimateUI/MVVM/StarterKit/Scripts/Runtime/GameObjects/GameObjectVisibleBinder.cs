using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.GameObjects
{
    public class GameObjectVisibleBinder : MonoBinder, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private bool _isInvert;
        
        public void SetValue(bool value)
        {
            if (_isInvert) value = !_isInvert;
            gameObject.SetActive(value);
        }
    }
}