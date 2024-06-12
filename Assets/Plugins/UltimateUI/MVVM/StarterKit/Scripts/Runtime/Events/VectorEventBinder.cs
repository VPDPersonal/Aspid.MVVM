using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Events
{
    public class VectorEventBinder : MonoBinder, IBinder<Vector2>, IBinder<Vector3>
    {
        public event UnityAction<Vector2> Vector2ValueSet
        {
            add => _vector2ValueSet.AddListener(value);
            remove => _vector2ValueSet.RemoveListener(value);
        }
        
        public event UnityAction<Vector3> Vector3ValueSet
        {
            add => _vector3ValueSet.AddListener(value);
            remove => _vector3ValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector2> _vector2ValueSet;
        [SerializeField] private UnityEvent<Vector3> _vector3ValueSet;
        
        public void SetValue(Vector2 value) =>
            _vector2ValueSet?.Invoke(value);
        
        public void SetValue(Vector3 value) =>
            _vector3ValueSet?.Invoke(value);
    }
}