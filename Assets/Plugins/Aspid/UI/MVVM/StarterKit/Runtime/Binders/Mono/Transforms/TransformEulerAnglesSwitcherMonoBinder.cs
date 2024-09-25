using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Euler Angles Switcher")]
    public sealed class TransformEulerAnglesSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [SerializeField] private Space _space;

        protected override void SetValue(Vector3 value)
        {
            switch (_space)
            {
                case Space.Self: transform.localEulerAngles = value; break;
                case Space.World: transform.eulerAngles = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}