using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterQuaternion;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenuByType(typeof(Quaternion))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Quaternion")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Quaternion")]
    public sealed partial class UnityEventQuaternionMonoBinder : MonoBinder, IBinder<Quaternion>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [SerializeField] private UnityEvent<Quaternion> _set;
        
        [BinderLog]
        public void SetValue(Quaternion value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}