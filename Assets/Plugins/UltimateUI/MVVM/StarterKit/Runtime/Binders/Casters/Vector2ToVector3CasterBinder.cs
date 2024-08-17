using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Unity.Generation;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Casters
{
    [AddComponentMenu("UI/Binders/Casters/Vector2 To Vector3 Caster Binder")]
    public partial class Vector2ToVector3CasterBinder : MonoBinder, IBinder<Vector2>
    {
        // TODO Add Validate
        [Header("Binders")]
        [RequireBinder(typeof(Vector3))]
        [SerializeField] private MonoBinder[] _binders;
        
        [Header("Parameters")]
        [SerializeField] private Values _values;
        [SerializeField] private float _thirdValue;
        
        [BinderLog]
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