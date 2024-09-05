using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.GameObjects
{
    public partial class GameObjectTagMonoBinder : MonoBinder, IBinder<string>
    {
        [field: Header("Converter")]
        [field: SerializeField]
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterStringToString Converter { get; private set; }

        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = Converter?.Convert(value) ?? value;
    }
}