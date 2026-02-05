using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Bool By Bind")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Bool By Bind")]
    public sealed class UnityEventBoolByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [SerializeField] private bool _isInvert;
        [SerializeField] private UnityEvent<bool> _set;

        private void OnValidate() =>
            SetVisible();

        private void OnEnable() =>
            SetVisible();

        protected override void OnBound() => 
            SetVisible();

        protected override void OnUnbound() => 
            SetVisible();

        public void SetValue<T>(T value) { }

        private void SetVisible() =>
            _set?.Invoke(_isInvert ? !IsBound : IsBound);
    }
}