using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Binders.GameObjects
{
    public class GameObjectVisibleBinder : Binder, IBinder<bool>
    {
        protected readonly bool IsInvert;
        protected readonly GameObject GameObject;
        
        public GameObjectVisibleBinder(GameObject gameObject, bool isInvert = false)
        {
            IsInvert = isInvert;
            GameObject = gameObject;
        }
        
        public void SetValue(bool value)
        {
            if (IsInvert) value = !value;
            GameObject.SetActive(value);
        }
    }
}