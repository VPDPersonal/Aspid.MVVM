using UnityEngine;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Scale Switcher")]
    public sealed class TransformScaleSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        protected override void SetValue(Vector3 value) =>
            transform.localScale = value;
    }
}