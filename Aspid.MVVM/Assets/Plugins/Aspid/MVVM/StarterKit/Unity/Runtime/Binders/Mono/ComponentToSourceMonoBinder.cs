using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder that reads the component reference itself back to the ViewModel
    /// in one-way-to-source mode when binding is established.
    /// </summary>
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    public abstract class ComponentToSourceMonoBinder<TComponent> : ComponentMonoBinder<TComponent>, IReverseBinder<TComponent>
        where TComponent : Component
    {
        public event Action<TComponent> ValueChanged;
        
        protected override void OnBound() =>
            ValueChanged?.Invoke(CachedComponent);
    }

    /// <summary>
    /// Concrete MonoBehaviour binder that sends the attached <see cref="UnityEngine.Component"/> reference back
    /// to the ViewModel when binding is established. Accepts any value type via <see cref="IAnyReverseBinder"/>.
    /// </summary>
    /// <remarks>
    /// Because the value is passed as <see cref="object"/>, there is no compile-time type check.
    /// It is the developer's responsibility to ensure the ViewModel property accepts a compatible type;
    /// a type mismatch will only surface at runtime.
    /// </remarks>
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