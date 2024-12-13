using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/GameObject/GameObject Binder - Tag Enum Group")]
    public sealed class GameObjectTagEnumGroupMonoBinder : EnumGroupMonoBinder<GameObject, string>
    {
        protected override void SetValue(GameObject component, string value) =>
            component.tag = value;
    }
}