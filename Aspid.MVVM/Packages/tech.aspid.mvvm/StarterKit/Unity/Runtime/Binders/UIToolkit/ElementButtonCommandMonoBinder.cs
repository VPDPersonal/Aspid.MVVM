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
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Button Command")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UIToolkit/Element Binder – Button Command")]
    public sealed partial class ElementButtonCommandMonoBinder : VisualElementMonoBinder<Button>, IBinder<IRelayCommand>
    {
        [Tooltip("When enabled, the button disables while the command cannot execute.")]
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
