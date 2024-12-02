using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    public interface IRotationReverseBinder : IVectorReverseBinder, IReverseBinder<Quaternion>
    {
        event Action<Quaternion> RotationValueChanged;
        
        event Action<Quaternion> IReverseBinder<Quaternion>.ValueChanged
        {
            add => RotationValueChanged += value;
            remove => RotationValueChanged -= value;
        }
    }
}