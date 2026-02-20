using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface IRotationBinder : IVectorBinder, IBinder<Quaternion>
    {
        void IBinder<Vector2>.SetValue(Vector2 value) =>
            SetValue(Quaternion.Euler(value));
        
        void IBinder<Vector3>.SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
    }
}