using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
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