using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/GameObject/GameObject Binder - Visible EnumGroup")]
    public sealed class GameObjectVisibleEnumGroupMonoBinder : EnumGroupMonoBinder<GameObject>
    {
        [Header("Parameters")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;
        
        protected override void SetDefaultValue(GameObject element) =>
            element.SetActive(_defaultValue);

        protected override void SetSelectedValue(GameObject element) =>
            element.SetActive(_selectedValue);
    }
}