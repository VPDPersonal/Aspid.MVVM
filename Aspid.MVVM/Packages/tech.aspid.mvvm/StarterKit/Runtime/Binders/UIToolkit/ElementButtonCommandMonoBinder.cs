#if ASPID_MVVM_HAS_UIELEMENTS
using UnityEngine;
using Button = UnityEngine.UIElements.Button;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TElement}"/> that executes a command when the <see cref="Button"/> is
    /// clicked.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Button Command")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UIToolkit/Element Binder – Button Command")]
    public sealed partial class ElementButtonCommandMonoBinder : VisualElementMonoBinder<Button>, IBinder<IRelayCommand>
    {
        [Tooltip("Disable the button while the command cannot execute.")]
        [SerializeField] private bool _isFollowCanExecute = true;

        private IRelayCommand _command;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand value)
        {
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

            var element = Element;
            if (element is null) return;

            element.clicked -= Execute;
            if (_command is not null) element.clicked += Execute;

            ApplyCanExecute();
        }

        /// <inheritdoc/>
        protected override void OnDetach()
        {
            if (Element is not null) Element.clicked -= Execute;

            SetValue(null);
            base.OnDetach();
        }

        /// <inheritdoc/>
        protected override void RestoreElement() =>
            SetValue(_command);

        /// <inheritdoc/>
        protected override void OnElementDetached(Button element) =>
            element.clicked -= Execute;

        private void Execute() =>
            _command?.Execute();

        private void OnCanExecuteChanged(IRelayCommand command) =>
            ApplyCanExecute();

        private void ApplyCanExecute()
        {
            if (!_isFollowCanExecute) return;

            var element = Element;
            if (element is not null)
                element.SetEnabled(_command?.CanExecute() ?? false);
        }
    }
}
#endif
