using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Slider}"/> that executes a command each time <see cref="Slider.onValueChanged"/> fires,
    /// passing the current slider value as the command argument.
    /// Accepts commands typed as <see cref="IRelayCommand{T}">IRelayCommand&lt;int&gt;</see>, <see cref="IRelayCommand{T}">IRelayCommand&lt;long&gt;</see>,
    /// <see cref="IRelayCommand{T}">IRelayCommand&lt;float&gt;</see> or <see cref="IRelayCommand{T}">IRelayCommand&lt;double&gt;</see>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Slider Binder – Command")]
    public sealed partial class SliderCommandMonoBinder : ComponentMonoBinder<Slider>, 
        IBinder<IRelayCommand<int>>, 
        IBinder<IRelayCommand<long>>,
        IBinder<IRelayCommand<float>>,
        IBinder<IRelayCommand<double>>
    {
        [Tooltip("How CanExecute affects the slider's interactable state.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("View that reflects CanExecute when Interactable Mode is Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<int> _intCommand;
        private IRelayCommand<long> _longCommand;
        private IRelayCommand<float> _floatCommand;
        private IRelayCommand<double> _doubleCommand;

        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
            else if (_floatCommand is not null) OnCanExecuteChanged(_floatCommand);
            else if (_doubleCommand is not null) OnCanExecuteChanged(_doubleCommand);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;int&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<int> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;long&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<long> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;float&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<float> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;double&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<double> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Slider.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        /// <remarks>
        /// The subscription connects the slider's value change event to OnValueChanged, which
        /// dispatches to the first non-null command among all bound command types.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Slider.onValueChanged"/>
        /// and releases all bound command references.
        /// </summary>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int>)null);
            SetValue((IRelayCommand<long>)null);
            SetValue((IRelayCommand<float>)null);
            SetValue((IRelayCommand<double>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(CachedComponent.value);
            else if (_intCommand is not null) _intCommand.Execute((int)CachedComponent.value);
            else if (_doubleCommand is not null) _doubleCommand.Execute(CachedComponent.value);
            else if (_longCommand is not null) _longCommand.Execute((long)CachedComponent.value);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int> command) =>
            ApplyCanExecute(command, (int)CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<long> command) =>
            ApplyCanExecute(command, (long)CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<float> command) =>
            ApplyCanExecute(command, CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<double> command) =>
            ApplyCanExecute(command, (double)CachedComponent.value);

        private void ApplyCanExecute<T>(IRelayCommand<T> command, T value)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(value));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Slider}"/> that executes a command each time <see cref="Slider.onValueChanged"/> fires,
    /// passing the current slider value and an additional parameter as the command arguments.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;int, T&gt;</see>, <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;long, T&gt;</see>,
    /// <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;float, T&gt;</see> or <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;double, T&gt;</see>.
    /// </summary>
    /// <typeparam name="T">The type of the additional parameter forwarded alongside the slider value.</typeparam>
    public abstract partial class SliderCommandMonoBinder<T> : ComponentMonoBinder<Slider>, 
        IBinder<IRelayCommand<int, T>>,
        IBinder<IRelayCommand<long, T>>, 
        IBinder<IRelayCommand<float, T>>, 
        IBinder<IRelayCommand<double, T>>
    {
        [Tooltip("Extra parameter forwarded alongside the slider value.")]
        [SerializeField] private T _param;
        
        [Tooltip("How CanExecute affects the slider's interactable state.")]
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("View that reflects CanExecute when Interactable Mode is Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<int, T> _intCommand;
        private IRelayCommand<long, T> _longCommand;
        private IRelayCommand<float, T> _floatCommand;
        private IRelayCommand<double, T> _doubleCommand;
        
        /// <summary>
        /// Gets or sets the additional parameter forwarded alongside the slider value.
        /// </summary>
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
            else if (_floatCommand is not null) OnCanExecuteChanged(_floatCommand);
            else if (_doubleCommand is not null) OnCanExecuteChanged(_doubleCommand);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;int, T&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<int, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;long, T&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<long, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;float, T&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<float, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;double, T&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<double, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Slider.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        /// <remarks>
        /// The subscription connects the slider's value change event to OnValueChanged, which
        /// dispatches to the first non-null command among all bound command types.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Slider.onValueChanged"/>
        /// and releases all bound command references.
        /// </summary>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int, T>)null);
            SetValue((IRelayCommand<long, T>)null);
            SetValue((IRelayCommand<float, T>)null);
            SetValue((IRelayCommand<double, T>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(CachedComponent.value, Param);
            else if (_intCommand is not null) _intCommand.Execute((int)CachedComponent.value, Param);
            else if (_doubleCommand is not null) _doubleCommand.Execute(CachedComponent.value, Param);
            else if (_longCommand is not null) _longCommand.Execute((long)CachedComponent.value, Param);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T> command) =>
            ApplyCanExecute(command, (int)CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<long, T> command) =>
            ApplyCanExecute(command, (long)CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<float, T> command) =>
            ApplyCanExecute(command, CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<double, T> command) =>
            ApplyCanExecute(command, (double)CachedComponent.value);

        private void ApplyCanExecute<TValue>(IRelayCommand<TValue, T> command, TValue value)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(value, Param));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
        
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Slider}"/> that executes a command each time <see cref="Slider.onValueChanged"/> fires,
    /// passing the current slider value and two additional parameters as the command arguments.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;int, T1, T2&gt;</see>, <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;long, T1, T2&gt;</see>,
    /// <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;float, T1, T2&gt;</see> or <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;double, T1, T2&gt;</see>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter.</typeparam>
    public abstract partial class SliderCommandMonoBinder<T1, T2> : ComponentMonoBinder<Slider>, 
        IBinder<IRelayCommand<int, T1, T2>>,
        IBinder<IRelayCommand<long, T1, T2>>, 
        IBinder<IRelayCommand<float, T1, T2>>, 
        IBinder<IRelayCommand<double, T1, T2>>
    {
        [Tooltip("First extra parameter forwarded alongside the slider value.")]
        [SerializeField] private T1 _param1;
        [Tooltip("Second extra parameter forwarded alongside the slider value.")]
        [SerializeField] private T2 _param2;
        
        [Tooltip("How CanExecute affects the slider's interactable state.")]
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("View that reflects CanExecute when Interactable Mode is Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<int, T1, T2> _intCommand;
        private IRelayCommand<long, T1, T2> _longCommand;
        private IRelayCommand<float, T1, T2> _floatCommand;
        private IRelayCommand<double, T1, T2> _doubleCommand;
        
        /// <summary>
        /// Gets or sets the first additional parameter.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        /// <summary>
        /// Gets or sets the second additional parameter.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
            else if (_floatCommand is not null) OnCanExecuteChanged(_floatCommand);
            else if (_doubleCommand is not null) OnCanExecuteChanged(_doubleCommand);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;int, T1, T2&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;long, T1, T2&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;float, T1, T2&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<float, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;double, T1, T2&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<double, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Slider.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        /// <remarks>
        /// The subscription connects the slider's value change event to OnValueChanged, which
        /// dispatches to the first non-null command among all bound command types.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Slider.onValueChanged"/>
        /// and releases all bound command references.
        /// </summary>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int, T1, T2>)null);
            SetValue((IRelayCommand<long, T1, T2>)null);
            SetValue((IRelayCommand<float, T1, T2>)null);
            SetValue((IRelayCommand<double, T1, T2>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(CachedComponent.value, Param1, Param2);
            else if (_intCommand is not null) _intCommand.Execute((int)CachedComponent.value, Param1, Param2);
            else if (_doubleCommand is not null) _doubleCommand.Execute(CachedComponent.value, Param1, Param2);
            else if (_longCommand is not null) _longCommand.Execute((long)CachedComponent.value, Param1, Param2);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2> command) =>
            ApplyCanExecute(command, (int)CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<long, T1, T2> command) =>
            ApplyCanExecute(command, (long)CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2> command) =>
            ApplyCanExecute(command, CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<double, T1, T2> command) =>
            ApplyCanExecute(command, (double)CachedComponent.value);

        private void ApplyCanExecute<TValue>(IRelayCommand<TValue, T1, T2> command, TValue value)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(value, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
    
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Slider}"/> that executes a command each time <see cref="Slider.onValueChanged"/> fires,
    /// passing the current slider value and three additional parameters as the command arguments.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;int, T1, T2, T3&gt;</see>, <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;long, T1, T2, T3&gt;</see>,
    /// <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;float, T1, T2, T3&gt;</see> or <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;double, T1, T2, T3&gt;</see>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter.</typeparam>
    public abstract partial class SliderCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<Slider>, 
        IBinder<IRelayCommand<int, T1, T2, T3>>,
        IBinder<IRelayCommand<long, T1, T2, T3>>, 
        IBinder<IRelayCommand<float, T1, T2, T3>>, 
        IBinder<IRelayCommand<double, T1, T2, T3>>
    {
        [Tooltip("First extra parameter forwarded alongside the slider value.")]
        [SerializeField] private T1 _param1;
        [Tooltip("Second extra parameter forwarded alongside the slider value.")]
        [SerializeField] private T2 _param2;
        [Tooltip("Third extra parameter forwarded alongside the slider value.")]
        [SerializeField] private T3 _param3;
        
        [Tooltip("How CanExecute affects the slider's interactable state.")]
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("View that reflects CanExecute when Interactable Mode is Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<int, T1, T2, T3> _intCommand;
        private IRelayCommand<long, T1, T2, T3> _longCommand;
        private IRelayCommand<float, T1, T2, T3> _floatCommand;
        private IRelayCommand<double, T1, T2, T3> _doubleCommand;
        
        /// <summary>
        /// Gets or sets the first additional parameter.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        /// <summary>
        /// Gets or sets the second additional parameter.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        /// <summary>
        /// Gets or sets the third additional parameter.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
            else if (_floatCommand is not null) OnCanExecuteChanged(_floatCommand);
            else if (_doubleCommand is not null) OnCanExecuteChanged(_doubleCommand);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;int, T1, T2, T3&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;long, T1, T2, T3&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;float, T1, T2, T3&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<float, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;double, T1, T2, T3&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<double, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Slider.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        /// <remarks>
        /// The subscription connects the slider's value change event to OnValueChanged, which
        /// dispatches to the first non-null command among all bound command types.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Slider.onValueChanged"/>
        /// and releases all bound command references.
        /// </summary>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int, T1, T2, T3>)null);
            SetValue((IRelayCommand<long, T1, T2, T3>)null);
            SetValue((IRelayCommand<float, T1, T2, T3>)null);
            SetValue((IRelayCommand<double, T1, T2, T3>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(CachedComponent.value, Param1, Param2, Param3);
            else if (_intCommand is not null) _intCommand.Execute((int)CachedComponent.value, Param1, Param2, Param3);
            else if (_doubleCommand is not null) _doubleCommand.Execute(CachedComponent.value, Param1, Param2, Param3);
            else if (_longCommand is not null) _longCommand.Execute((long)CachedComponent.value, Param1, Param2, Param3);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2, T3> command) =>
            ApplyCanExecute(command, (int)CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<long, T1, T2, T3> command) =>
            ApplyCanExecute(command, (long)CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2, T3> command) =>
            ApplyCanExecute(command, CachedComponent.value);

        private void OnCanExecuteChanged(IRelayCommand<double, T1, T2, T3> command) =>
            ApplyCanExecute(command, (double)CachedComponent.value);

        private void ApplyCanExecute<TValue>(IRelayCommand<TValue, T1, T2, T3> command, TValue value)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(value, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
}