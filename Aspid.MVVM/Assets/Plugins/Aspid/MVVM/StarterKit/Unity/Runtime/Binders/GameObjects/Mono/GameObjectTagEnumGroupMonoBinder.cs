using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Tag EnumGroup")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Tag EnumGroup")]
    public sealed class GameObjectTagEnumGroupMonoBinder : EnumGroupMonoBinder<GameObject>
    {
        [Header("Values")]
        [SerializeField] private string _defaultValue;
        [SerializeField] private string _selectedValue;
        
        protected override void SetDefaultValue(GameObject element) =>
            element.tag = _defaultValue;

        protected override void SetSelectedValue(GameObject element) =>
            element.tag = _selectedValue;
    }
}