using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public abstract class MonoView : MonoBehaviour, IView
    {
        // TODO Add
// #if UNITY_EDITOR
//         [HideInInspector]
//         [SerializeField] private List<Binder> _otherBinders;
// #endif

        protected virtual void OnValidate() =>
            ViewUtility.ValidateBinders(this);

        public virtual IReadOnlyDictionary<string, IReadOnlyList<IBinder>> GetBinders() =>
            throw new Exception("This method must be implemented in the inheritor");
    }
}