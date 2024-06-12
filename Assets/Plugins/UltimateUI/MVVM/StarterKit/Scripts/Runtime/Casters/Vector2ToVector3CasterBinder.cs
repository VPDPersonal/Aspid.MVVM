using System;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Casters
{
    public class Vector2ToVector3CasterBinder : MonoBinder, IBinder<Vector2>
    {
        // TODO Add Validate
        [Header("Binders")]
        [SerializeField] private MonoBinder[] _binders;
        
        [Header("Parameters")]
        [SerializeField] private Values _values;
        [SerializeField] private float _thirdValue;
        
        public void SetValue(Vector2 value)
        {
            var vector3 = _values switch
            {
                Values.XY => new Vector3(value.x, value.y, _thirdValue),
                Values.XZ => new Vector3(value.x, _thirdValue, value.y),
                Values.YZ => new Vector3(_thirdValue, value.x, value.y),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            foreach (var binder in _binders)
            {
                if (binder is IBinder<Vector3> vector3Binder)
                    vector3Binder.SetValue(vector3);
            }
        }
        
        private enum Values
        {
            XY,
            XZ,
            YZ,
        }
    }
}