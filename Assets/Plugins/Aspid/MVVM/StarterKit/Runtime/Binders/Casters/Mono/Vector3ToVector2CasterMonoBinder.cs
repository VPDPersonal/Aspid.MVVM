using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Vector3, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterVector3ToVector2;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Vector3 To Vector2 Caster Binder")]
    public sealed partial class Vector3ToVector2CasterMonoBinder : MonoBinder, IBinder<Vector2>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new Vector3ToVector2Converter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector3> _casted;
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
}