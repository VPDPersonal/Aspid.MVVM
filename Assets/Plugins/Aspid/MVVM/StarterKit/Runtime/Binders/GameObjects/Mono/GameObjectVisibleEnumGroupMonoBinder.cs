using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/GameObject/GameObject Binder - Visible Enum Group")]
    public sealed class GameObjectVisibleEnumGroupMonoBinder : EnumGroupMonoBinder<GameObject>
    {
        [SerializeField] private bool _isInvert;
        
        protected override void SetDefaultValue(GameObject component) =>
            component.SetActive(_isInvert);

        protected override void SetSelectedValue(GameObject component) =>
            component.SetActive(!_isInvert);
    }
}