using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder - Enabled Enum")]
    [AddComponentContextMenu(typeof(Behaviour),"Add Behaviour Binder/Behaviour Binder - Enabled Enum")]
    public sealed class BehaviourEnabledEnumMonoBinder : EnumMonoBinder<Behaviour, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}