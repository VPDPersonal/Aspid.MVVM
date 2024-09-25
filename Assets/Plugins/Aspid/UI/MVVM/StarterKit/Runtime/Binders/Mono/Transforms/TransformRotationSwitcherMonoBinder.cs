using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Rotation Switcher")]
    public sealed class TransformRotationSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [SerializeField] private Space _space;

        protected override void SetValue(Vector3 value)
        {
            switch (_space)
            {
                case Space.Self: transform.localRotation = Quaternion.Euler(value); break;
                case Space.World: transform.rotation = Quaternion.Euler(value);; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}