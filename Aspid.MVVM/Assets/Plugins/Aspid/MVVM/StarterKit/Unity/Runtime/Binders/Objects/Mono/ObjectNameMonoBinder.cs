using UnityEngine;
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
    public partial class ObjectNameMonoBinder : MonoBinder, IBinder<string>
    {
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
            _object.name = _converter?.Convert(value) ?? value ?? string.Empty;
    }
}