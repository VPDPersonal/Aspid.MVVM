using UnityEngine;
using AspidUI.MVVM.Unity.Generation;
using AspidUI.MVVM.StarterKit.Converters;
using MonoBinder = AspidUI.MVVM.Unity.MonoBinder;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.GameObjects
{
    public partial class GameObjectTagMonoBinder : MonoBinder, IBinder<string>
    {
        [field: Header("Converter")]
        [field: SerializeField]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterStringToString Converter { get; private set; }

        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = Converter?.Convert(value) ?? value;
    }
}