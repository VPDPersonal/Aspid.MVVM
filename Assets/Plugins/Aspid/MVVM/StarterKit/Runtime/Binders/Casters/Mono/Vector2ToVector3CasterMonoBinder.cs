using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Casters/Vector2 To Vector3 Caster Binder")]
    public sealed partial class Vector2ToVector3CasterMonoBinder : MonoBinder, IBinder<Vector2>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Vector2, Vector3> _converter = new Vector2ToVector3Converter();
#else
        [SerializeReference] private IConverterVector2ToVector3 _converter = new Vector2ToVector3Converter();
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector3> _casted;
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
}