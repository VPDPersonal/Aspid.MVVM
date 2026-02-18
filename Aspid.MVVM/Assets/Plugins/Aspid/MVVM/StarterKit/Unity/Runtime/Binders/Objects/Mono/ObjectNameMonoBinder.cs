using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Object/Object Binder – Name")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Object/Object Binder – Name")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed partial class ObjectNameMonoBinder : MonoBinder,
        IBinder<string>,
        IReverseBinder<string>
    {
        public event Action<string> ValueChanged;
        
        [SerializeField] private Object _object;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        private void OnValidate()
        {
            if (!_object)
                _object = gameObject;
        }

        [BinderLog]
        public void SetValue(string value) =>
            _object.name = GetConvertedValue(value);

        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(_object.name));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value ?? string.Empty;
    }
}