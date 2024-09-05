using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;
using UltimateUI.MVVM.StarterKit.Converters.Vectors;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Casters
{
    [AddComponentMenu("UI/Binders/Casters/Vector2 To Vector3 Caster Binder")]
    public sealed partial class Vector2ToVector3CasterBinder : MonoBinder, IBinder<Vector2>
    {
        [Header("Converter")]
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
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