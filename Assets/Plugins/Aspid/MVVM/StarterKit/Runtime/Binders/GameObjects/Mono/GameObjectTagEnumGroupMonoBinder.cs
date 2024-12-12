using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/GameObject/GameObject Binder - Tag Enum Group")]
    public sealed class GameObjectTagEnumGroupMonoBinder : EnumGroupMonoBinder<GameObject>
    {
        [Header("Parameters")]
        [SerializeField] private string _defaultValue;
        [SerializeField] private string _selectedValue;
        
        protected override void SetDefaultValue(GameObject component) =>
            component.tag = _defaultValue;

        protected override void SetSelectedValue(GameObject component) =>
            component.tag = _selectedValue;
    }
}