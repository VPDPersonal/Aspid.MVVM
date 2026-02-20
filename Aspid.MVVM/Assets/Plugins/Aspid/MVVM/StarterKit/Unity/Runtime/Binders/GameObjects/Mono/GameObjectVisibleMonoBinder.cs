using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Visible")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed partial class GameObjectVisibleMonoBinder : MonoBinder, 
        IBinder<bool>, 
        IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            gameObject.SetActive(GetConvertedValue(value));
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(gameObject.activeSelf));
        }

        private bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}