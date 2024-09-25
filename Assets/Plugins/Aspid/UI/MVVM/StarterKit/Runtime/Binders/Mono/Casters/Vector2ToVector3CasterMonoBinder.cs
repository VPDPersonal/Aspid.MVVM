using UnityEngine;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;
using Aspid.UI.MVVM.StarterKit.Converters.Vectors;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Casters
{
    [AddComponentMenu("UI/Binders/Casters/Vector2 To Vector3 Caster Binder")]
    public sealed partial class Vector2ToVector3CasterMonoBinder : MonoBinder, IBinder<Vector2>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterVectorToVector3 _converter = new Vector2ToVector3Converter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector3> _casted;
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
}