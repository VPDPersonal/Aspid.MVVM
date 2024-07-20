using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Position Switcher")]
    public partial class TransformPositionSwitcherBinder : TransformBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Space _space;
        [SerializeField] private Vector3 _truePosition;
        [SerializeField] private Vector3 _falsePosition;
        
        protected Space Space => _space;

        protected Vector3 TruePosition => _truePosition;
        
        protected Vector3 FalsePosition => _falsePosition;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            switch (Space)
            {
                case Space.Self: CachedTransform.localPosition = GetPosition(value); break;
                case Space.World: CachedTransform.position = GetPosition(value); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected Vector3 GetPosition(bool value) =>
            value ? TruePosition : FalsePosition;
    }
}