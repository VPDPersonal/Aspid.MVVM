using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Bool By Bind")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/UnityEvent/UnityEvent Binder - Bool By Bind")]
    public sealed class UnityEventBoolByBindMonoBinder : MonoBinder, IAnyBinder
    {
        public event UnityAction<bool> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [SerializeField] private bool _isInvert;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _set;

        private void OnValidate() =>
            SetVisible();

        private void OnEnable() =>
            SetVisible();

        protected override void OnBound() => SetVisible();

        protected override void OnUnbound() => SetVisible();

        public void SetValue<T>(T value) { }

        private void SetVisible() =>
            _set?.Invoke(_isInvert ? !IsBound : IsBound);
    }
}