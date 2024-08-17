using System;
using UnityEngine;
using UltimateUI.MVVM.Unity.Generation;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Rotation")]
    public partial class TransformRotationMonoBinder : TransformMonoBinderBase, IRotationBinder
    {
        [Header("Parameters")]
        [SerializeField] private Space _space;

        protected Space Space => _space;
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        [BinderLog]
        public void SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
        
        [BinderLog]
        public void SetValue(Quaternion value)
        {
            switch (Space)
            {
                case Space.Self: CachedTransform.localRotation = value; break;
                case Space.World: CachedTransform.rotation = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}