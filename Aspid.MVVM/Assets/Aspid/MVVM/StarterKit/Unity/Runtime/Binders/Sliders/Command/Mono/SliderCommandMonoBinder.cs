using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Slider}"/> that executes a command each time <see cref="Slider.onValueChanged"/> fires,
    /// passing the current slider value as the command argument.
    /// Accepts commands typed as <see cref="IRelayCommand{int}"/>, <see cref="IRelayCommand{long}"/>,
    /// <see cref="IRelayCommand{float}"/> or <see cref="IRelayCommand{double}"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-Slider-Command-1.1.0.xml" path="doc//member[@name='SliderCommandMonoBinder']/*" />
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Slider Binder – Command")]
    public sealed partial class SliderCommandMonoBinder : ComponentMonoBinder<Slider>, 
        IBinder<IRelayCommand<int>>, 
        IBinder<IRelayCommand<long>>,
        IBinder<IRelayCommand<float>>,
        IBinder<IRelayCommand<double>>
    {
        [Tooltip("Controls how the slider's interactable state reflects the command's CanExecute result.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("The view used to reflect the command's CanExecute state when InteractableMode is Custom.")]
        [SerializeReferenceDropdown]
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
        /// Binds an <see cref="IRelayCommand{int}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<int> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{long}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<long> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{float}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<float> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{double}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
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
        /// <remarks>
        /// Passes <see langword="null"/> to each SetValue overload to detach command
        /// references and unsubscribe from their <see cref="IRelayCommand.CanExecuteChanged"/> events.
        /// </remarks>
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
        
        private void OnCanExecuteChanged<T>(IRelayCommand<T> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = CachedComponent.value;
            
            // Safe only when sizeof(T) <= sizeof(float) (i.e. T is int or float).
            // Unsafe.As throws at runtime for T = long or double (8 bytes > 4 bytes).
            var castedValue = Unsafe.As<float, T>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }

    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Slider}"/> that executes a command each time <see cref="Slider.onValueChanged"/> fires,
    /// passing the current slider value and an additional parameter as the command arguments.
    /// Accepts commands typed as <see cref="IRelayCommand{int,T}"/>, <see cref="IRelayCommand{long,T}"/>,
    /// <see cref="IRelayCommand{float,T}"/> or <see cref="IRelayCommand{double,T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the additional parameter forwarded alongside the slider value.</typeparam>
    /// <include file="XmlExampleDoc-Slider-Command-1.1.0.xml" path="doc//member[@name='SliderCommandMonoBinderT']/*" />
    public abstract partial class SliderCommandMonoBinder<T> : ComponentMonoBinder<Slider>, 
        IBinder<IRelayCommand<int, T>>,
        IBinder<IRelayCommand<long, T>>, 
        IBinder<IRelayCommand<float, T>>, 
        IBinder<IRelayCommand<double, T>>
    {
        [Tooltip("The additional parameter forwarded alongside the slider value when the command is executed.")]
        [SerializeField] private T _param;
        
        [Tooltip("Controls how the slider's interactable state reflects the command's CanExecute result.")]
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("The view used to reflect the command's CanExecute state when InteractableMode is Custom.")]
        [SerializeReferenceDropdown]
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
        /// Binds an <see cref="IRelayCommand{int,T}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<int, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{long,T}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<long, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{float,T}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<float, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{double,T}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
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
        /// <remarks>
        /// Passes <see langword="null"/> to each SetValue overload to detach command
        /// references and unsubscribe from their <see cref="IRelayCommand.CanExecuteChanged"/> events.
        /// </remarks>
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
        
        private void OnCanExecuteChanged<TValue>(IRelayCommand<TValue, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = CachedComponent.value;
            
            // Safe only when sizeof(T) <= sizeof(float) (i.e. T is int or float).
            // Unsafe.As throws at runtime for T = long or double (8 bytes > 4 bytes).
            var castedValue = Unsafe.As<float, TValue>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue, Param));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
        
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Slider}"/> that executes a command each time <see cref="Slider.onValueChanged"/> fires,
    /// passing the current slider value and two additional parameters as the command arguments.
    /// Accepts commands typed as <see cref="IRelayCommand{int,T1,T2}"/>, <see cref="IRelayCommand{long,T1,T2}"/>,
    /// <see cref="IRelayCommand{float,T1,T2}"/> or <see cref="IRelayCommand{double,T1,T2}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter.</typeparam>
    /// <include file="XmlExampleDoc-Slider-Command-1.1.0.xml" path="doc//member[@name='SliderCommandMonoBinderT1T2']/*" />
    public abstract partial class SliderCommandMonoBinder<T1, T2> : ComponentMonoBinder<Slider>, 
        IBinder<IRelayCommand<int, T1, T2>>,
        IBinder<IRelayCommand<long, T1, T2>>, 
        IBinder<IRelayCommand<float, T1, T2>>, 
        IBinder<IRelayCommand<double, T1, T2>>
    {
        [Tooltip("The first additional parameter forwarded alongside the slider value when the command is executed.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second additional parameter forwarded alongside the slider value when the command is executed.")]
        [SerializeField] private T2 _param2;
        
        [Tooltip("Controls how the slider's interactable state reflects the command's CanExecute result.")]
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("The view used to reflect the command's CanExecute state when InteractableMode is Custom.")]
        [SerializeReferenceDropdown]
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
        /// Binds an <see cref="IRelayCommand{int,T1,T2}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{long,T1,T2}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{float,T1,T2}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<float, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{double,T1,T2}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
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
        /// <remarks>
        /// Passes <see langword="null"/> to each SetValue overload to detach command
        /// references and unsubscribe from their <see cref="IRelayCommand.CanExecuteChanged"/> events.
        /// </remarks>
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
        
        private void OnCanExecuteChanged<TValue>(IRelayCommand<TValue, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = CachedComponent.value;
            
            // Safe only when sizeof(T) <= sizeof(float) (i.e. T is int or float).
            // Unsafe.As throws at runtime for T = long or double (8 bytes > 4 bytes).
            var castedValue = Unsafe.As<float, TValue>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Slider}"/> that executes a command each time <see cref="Slider.onValueChanged"/> fires,
    /// passing the current slider value and three additional parameters as the command arguments.
    /// Accepts commands typed as <see cref="IRelayCommand{int,T1,T2,T3}"/>, <see cref="IRelayCommand{long,T1,T2,T3}"/>,
    /// <see cref="IRelayCommand{float,T1,T2,T3}"/> or <see cref="IRelayCommand{double,T1,T2,T3}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter.</typeparam>
    /// <include file="XmlExampleDoc-Slider-Command-1.1.0.xml" path="doc//member[@name='SliderCommandMonoBinderT1T2T3']/*" />
    public abstract partial class SliderCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<Slider>, 
        IBinder<IRelayCommand<int, T1, T2, T3>>,
        IBinder<IRelayCommand<long, T1, T2, T3>>, 
        IBinder<IRelayCommand<float, T1, T2, T3>>, 
        IBinder<IRelayCommand<double, T1, T2, T3>>
    {
        [Tooltip("The first additional parameter forwarded alongside the slider value when the command is executed.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second additional parameter forwarded alongside the slider value when the command is executed.")]
        [SerializeField] private T2 _param2;
        [Tooltip("The third additional parameter forwarded alongside the slider value when the command is executed.")]
        [SerializeField] private T3 _param3;
        
        [Tooltip("Controls how the slider's interactable state reflects the command's CanExecute result.")]
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("The view used to reflect the command's CanExecute state when InteractableMode is Custom.")]
        [SerializeReferenceDropdown]
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
        /// Binds an <see cref="IRelayCommand{int,T1,T2,T3}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{long,T1,T2,T3}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{float,T1,T2,T3}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<float, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{double,T1,T2,T3}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
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
        /// <remarks>
        /// Passes <see langword="null"/> to each SetValue overload to detach command
        /// references and unsubscribe from their <see cref="IRelayCommand.CanExecuteChanged"/> events.
        /// </remarks>
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
        
        private void OnCanExecuteChanged<TValue>(IRelayCommand<TValue, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = CachedComponent.value;
            
            // Safe only when sizeof(T) <= sizeof(float) (i.e. T is int or float).
            // Unsafe.As throws at runtime for T = long or double (8 bytes > 4 bytes).
            var castedValue = Unsafe.As<float, TValue>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
}