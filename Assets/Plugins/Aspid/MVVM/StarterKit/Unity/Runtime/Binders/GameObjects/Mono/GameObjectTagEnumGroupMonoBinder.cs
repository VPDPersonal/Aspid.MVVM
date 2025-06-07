using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Tag EnumGroup")]
    [AddComponentContextMenu(typeof(Component),"Add GameObject Binder/GameObject Binder - Tag EnumGroup")]
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