using UnityEngine;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Vector")]
    public sealed partial class VectorUnityEventMonoBinder : MonoBinder, IVectorBinder
    {
        public event UnityAction<Vector2> Vector2ValueSet
        {
            add => _vector2ValueSet.AddListener(value);
            remove => _vector2ValueSet.RemoveListener(value);
        }
        
        public event UnityAction<Vector3> Vector3ValueSet
        {
            add => _vector3ValueSet.AddListener(value);
            remove => _vector3ValueSet.RemoveListener(value);
        }
        
        // Vector2 Converter
        [Header("Converters")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Vector2, Vector2> _vector2Converter;
#else
        [SerializeReference] private IConverterVector2ToVector2 _vector2Converter;
#endif
        
        // Vector 3 Converter
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Vector3, Vector3> _vector3Converter;
#else
        [SerializeReference] private IConverterVector3ToVector3 _vector3Converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector2> _vector2ValueSet;
        [SerializeField] private UnityEvent<Vector3> _vector3ValueSet;
        
        [BinderLog]
        public void SetValue(Vector2 value) 
        {
            value = _vector2Converter?.Convert(value) ?? value;
            _vector2ValueSet?.Invoke(value);
        }

        [BinderLog]
        public void SetValue(Vector3 value)
        {
            value = _vector3Converter?.Convert(value) ?? value;
            _vector3ValueSet?.Invoke(value);
        }
    }
}