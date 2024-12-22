using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/GameObject/GameObject Binder - Tag EnumGroup")]
    public sealed class GameObjectTagEnumGroupMonoBinder : EnumGroupMonoBinder<GameObject>
    {
        [Header("Parameters")]
        [SerializeField] private string _defaultValue;
        [SerializeField] private string _selectedValue;
        
        protected override void SetDefaultValue(GameObject element) =>
            element.tag = _defaultValue;

        protected override void SetSelectedValue(GameObject element) =>
            element.tag = _selectedValue;
    }
}