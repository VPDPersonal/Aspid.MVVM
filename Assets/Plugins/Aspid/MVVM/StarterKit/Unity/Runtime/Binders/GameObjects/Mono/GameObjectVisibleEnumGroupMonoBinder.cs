using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Visible EnumGroup")]
    [AddComponentContextMenu(typeof(Component),"Add GameObject Binder/GameObject Binder - Visible EnumGroup")]
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