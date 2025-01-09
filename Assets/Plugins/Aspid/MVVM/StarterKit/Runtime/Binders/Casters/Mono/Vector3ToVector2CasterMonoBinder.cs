using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Casters/Vector3 To Vector2 Caster Binder")]
    public sealed partial class Vector3ToVector2CasterMonoBinder : MonoBinder, IBinder<Vector2>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector3, Vector2> _converter = new Vector3ToVector2Converter();
#else
        private IConverterVector3ToVector2 _converter = new Vector3ToVector2Converter();
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector3> _casted;
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
}