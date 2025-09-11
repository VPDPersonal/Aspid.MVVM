using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Tag Switcher")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/GameObject/GameObject Binder - Tag Switcher")]
    public sealed class GameObjectTagSwitcherMonoBinder : SwitcherMonoBinder<string>
    {
        protected override void SetValue(string value) =>
            gameObject.tag = value;
    }
}