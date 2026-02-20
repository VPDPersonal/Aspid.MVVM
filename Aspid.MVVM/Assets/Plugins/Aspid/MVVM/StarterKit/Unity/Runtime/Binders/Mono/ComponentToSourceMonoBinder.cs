using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    public abstract class ComponentToSourceMonoBinder<TComponent> : ComponentMonoBinder<TComponent>, IReverseBinder<TComponent>
        where TComponent : Component
    {
        public event Action<TComponent> ValueChanged;
        
        protected override void OnBound() =>
            ValueChanged?.Invoke(CachedComponent);
    }

    [AddComponentMenu("Aspid/MVVM/Binders/Components/Component To Source Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Component/Component To Source Binder")]
    public sealed class ComponentToSourceMonoBinder : ComponentToSourceMonoBinder<Component>, IAnyReverseBinder
    {
        public new event Action<object> ValueChanged;

        protected override void OnBound()
        {
            base.OnBound();
            ValueChanged?.Invoke(CachedComponent);
        }
    }
}