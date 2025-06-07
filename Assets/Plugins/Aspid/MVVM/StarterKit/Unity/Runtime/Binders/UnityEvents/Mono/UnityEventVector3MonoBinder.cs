using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterVector3;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Vector3))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Vector3")]
    [AddComponentContextMenu(typeof(Component),"Add UnityEvent Binder/UnityEvent Binder - Vector3")]
    public sealed partial class UnityEventVector3MonoBinder : MonoBinder, IBinder<Vector3>
    {
        public event UnityAction<Vector3> Vector3ValueSet
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }

        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector3> _set;

        [BinderLog]
        public void SetValue(Vector3 value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}