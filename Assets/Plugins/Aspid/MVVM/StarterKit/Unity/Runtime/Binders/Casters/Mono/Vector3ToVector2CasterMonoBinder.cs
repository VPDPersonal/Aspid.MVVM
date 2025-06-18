using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterVector3ToVector2;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Vector2))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Vector3 To Vector2 Caster Binder")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/Casters/Vector3 To Vector2 Caster Binder")]
    public sealed partial class Vector3ToVector2CasterMonoBinder : MonoBinder, IBinder<Vector3>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new Vector3ToVector2Converter();
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector2> _casted;
        
        [BinderLog]
        public void SetValue(Vector3 value) =>
            _casted?.Invoke(_converter.Convert(value));
    }
}