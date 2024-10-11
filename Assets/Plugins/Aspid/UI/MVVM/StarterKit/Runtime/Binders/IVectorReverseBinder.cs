using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IVectorReverseBinder : IReverseBinder<Vector2>, IReverseBinder<Vector3>
    {
        public event Action<Vector2> Vector2ValueChanged;
        public event Action<Vector3> Vector3ValueChanged;
        
        event Action<Vector2> IReverseBinder<Vector2>.ValueChanged
        {
            add => Vector2ValueChanged += value;
            remove => Vector2ValueChanged -= value;
        }
        
        event Action<Vector3> IReverseBinder<Vector3>.ValueChanged
        {
            add => Vector3ValueChanged += value;
            remove => Vector3ValueChanged -= value;
        }
    }
}