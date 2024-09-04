using System;
using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Position Switcher")]
    public sealed class TransformPositionSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [SerializeField] private Space _space;

        protected override void SetValue(Vector3 value)
        {
            switch (_space)
            {
                case Space.Self: transform.localPosition = value; break;
                case Space.World: transform.position = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}