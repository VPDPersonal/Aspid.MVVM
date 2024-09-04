using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Binders
{
    public interface IRotationBinder : IVectorBinder, IBinder<Quaternion>
    {
        void IBinder<Vector2>.SetValue(Vector2 value) =>
            SetValue(Quaternion.Euler(value));
        
        void IBinder<Vector3>.SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
    }
}