using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{Button}"/> implementing
    /// <see cref="IBinder{T}">IBinder&lt;IRelayCommand&gt;</see> that executes a command when the button is clicked.
    /// </summary>
    /// <remarks>
    /// The UI Toolkit counterpart of the button command binder. The button's enabled state follows the command's
    /// <see cref="IRelayCommand.CanExecute()"/>, the same way the uGUI one drives <c>interactable</c>, so a command that
    /// cannot run leaves a button that cannot be pressed.
    /// <para/>
    /// The subscription is taken when the binder is bound and released when it is unbound: a click handler that outlived
    /// the binding would execute a command belonging to a ViewModel the View no longer shows.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Button Command")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UIToolkit/Element Binder – Button Command")]
    public sealed partial class ElementButtonCommandMonoBinder : VisualElementMonoBinder<Button>, IBinder<IRelayCommand>
    {
        [Tooltip("When enabled, the button is disabled while the command reports it cannot execute.")]
        [SerializeField] private bool _isFollowCanExecute = true;

        private IRelayCommand _command;

        /// <summary>
        /// Binds the command the button executes.
        /// </summary>
        /// <param name="value">The command received from the ViewModel, or <see langword="null"/> to detach.</param>
        [BinderLog]
        public void SetValue(IRelayCommand value)
        {
            if (_command is not null) _command.CanExecuteChanged -= OnCanExecuteChanged;

            _command = value;

            if (_command is not null) _command.CanExecuteChanged += OnCanExecuteChanged;

            var element = Element;
            if (element is null) return;

            element.clicked -= Execute;
            if (_command is not null) element.clicked += Execute;

            ApplyCanExecute();
        }

        /// <summary>
        /// Called when the binder is unbound. Releases the command and the click subscription.
        /// </summary>
        protected override void OnUnbound()
        {
            if (Element is not null) Element.clicked -= Execute;
            if (_command is not null) _command.CanExecuteChanged -= OnCanExecuteChanged;

            _command = null;

            base.OnUnbound();
        }

        private void Execute() =>
            _command?.Execute();

        private void OnCanExecuteChanged(IRelayCommand command) =>
            ApplyCanExecute();

        private void ApplyCanExecute()
        {
            if (!_isFollowCanExecute) return;

            var element = Element;
            if (element is null) return;

            element.SetEnabled(_command?.CanExecute() ?? false);
        }
    }
}
