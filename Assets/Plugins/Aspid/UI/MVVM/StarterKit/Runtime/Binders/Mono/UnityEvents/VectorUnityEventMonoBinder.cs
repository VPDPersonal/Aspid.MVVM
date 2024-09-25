using UnityEngine;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.UnityEvents
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Vector")]
    public sealed partial class VectorUnityEventMonoBinder : Aspid.UI.MVVM.Mono.MonoBinder, IVectorBinder
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
        
        [Header("Converters")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterVector2ToVector2 _vector2Converter;
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterVector3ToVector3 _vector3Converter;
        
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