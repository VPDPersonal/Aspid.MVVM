using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/GameObject/GameObject Binder - Tag Switcher")]
    public sealed class GameObjectTagSwitcherMonoBinder : SwitcherMonoBinder<string>
    {
        protected override void SetValue(string value) =>
            gameObject.tag = value;
    }
}