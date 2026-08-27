using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that shows or hides the <see cref="GameObject"/> this component is attached to
    /// depending on whether the ViewModel exposes a matching binding field for this binder.
    /// </summary>
    [BindModeOverride(modes: BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible By Bind")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Visible By Bind")]
    public sealed partial class GameObjectVisibleByBindMonoBinder : MonoBinder, IAnyBinder
    {
        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneTime;

        [Tooltip("Inverts visibility (hide while bound). Ignores the bound value entirely.")]
        [SerializeField] private bool _isInvert;
        
        private void OnEnable() =>
            SetVisible();
        
        /// <summary>
        /// Called when binding is established. Applies the visibility that the new bound state implies.
        /// </summary>
        /// <remarks>
        /// This binder never reads a value — visibility follows whether a binding exists, so both this hook and
        /// its unbound counterpart do the same thing.
        /// </remarks>
        protected override void OnBound() => 
            SetVisible();

        /// <summary>
        /// Called when the binding is released. Applies the visibility that the new bound state implies.
        /// </summary>
        protected override void OnUnbound() =>
            SetVisible();

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value) { }
        
        private void SetVisible() =>
            gameObject.SetActive(_isInvert ? !IsBound : IsBound);
    }
}